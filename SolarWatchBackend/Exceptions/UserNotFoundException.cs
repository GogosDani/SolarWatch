namespace SolarWatch.Exceptions;

public class UserNotFoundException(string message) : Exception(message)
{
}