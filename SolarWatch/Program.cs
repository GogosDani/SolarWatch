using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SolarWatch.Data;
using SolarWatch.Services;
using SolarWatch.Services.Authentication;
using SolarWatch.Services.JsonParsers;
using SolarWatch.Services.Repositories;
using dotenv.net;



var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
builder.Configuration.AddEnvironmentVariables();


// Variables from usersecrets or appsettings
var connectionString = builder.Configuration["ConnectionString"];
var issuer = builder.Configuration["ValidIssuer"];
var audience = builder.Configuration["ValidAudience"];
var jwtSecretKey = builder.Configuration["JwtSecretKey"];


// Call builder functions
AddServices();
ConfigureSwagger();
AddDbContexts();
AddAuthentication();
AddIdentity();
AddCors();

// Middlewares
var app = builder.Build();

Migration();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowFrontend");

using var scope = app.Services.CreateScope(); 
var authenticationSeeder = scope.ServiceProvider.GetRequiredService<AuthenticationSeeder>();
authenticationSeeder.AddRoles();
authenticationSeeder.AddAdmin();
app.Run();



// BUILDER FUNCTIONS

void AddServices()
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSingleton<ICityParser, CityProcessor>();
    builder.Services.AddSingleton<ICityDataProvider, CityApiReader>();
    builder.Services.AddSingleton<ISolarParser, SolarProcessor>();
    builder.Services.AddSingleton<ISolarInfoProvider, SolarInfoReader>();
    builder.Services.AddScoped<ISolarRepository, SolarRepository>();
    builder.Services.AddScoped<ICityRepository, CityRepository>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddSingleton(new JwtSettings { SecretKey = jwtSecretKey });
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<AuthenticationSeeder>();

}

void ConfigureSwagger()
{
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    });
}

void AddDbContexts()
{
    builder.Services.AddDbContext<SolarApiContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
    builder.Services.AddDbContext<UsersContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
}

void AddAuthentication()
{
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSecretKey)
                ),
            };
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                    var roleClaim = claimsIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
                    if (roleClaim != null)
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, roleClaim.Value));
                    }
                    return Task.CompletedTask;
                }
            };
        });
}

void AddIdentity()
{
    builder.Services
        .AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<UsersContext>();
}

void AddCors()
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend",
            builder => builder
                .WithOrigins("http://localhost:4000")
                .AllowAnyHeader()
                .AllowAnyMethod());
    });

}

void Migration()
{
    using (var scope = app.Services.CreateScope())
    {
        var solarDb = scope.ServiceProvider.GetRequiredService<SolarApiContext>();
        var usersDb = scope.ServiceProvider.GetRequiredService<UsersContext>();
        solarDb.Database.Migrate();
        usersDb.Database.Migrate();
    }
}