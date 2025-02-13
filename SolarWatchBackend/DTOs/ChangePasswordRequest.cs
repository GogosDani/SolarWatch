namespace SolarWatch.DTOs;

public class ChangePasswordRequest
{
    public string CurrentPassword { get; init; }
    public string NewPassword { get; init; }

}