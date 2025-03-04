namespace SolarWatch.Services.ProfilePicture;

public interface IS3Service
{
    public Task<string> UploadFileAsync(Stream fileStream, string fileName);
}