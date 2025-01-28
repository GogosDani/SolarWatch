using System.ComponentModel.DataAnnotations;

namespace SolarWatch.Contracts;

public record RegistrationRequest(
    [Required]string Username,
    [Required]string Email,
    [Required]string Password
    );