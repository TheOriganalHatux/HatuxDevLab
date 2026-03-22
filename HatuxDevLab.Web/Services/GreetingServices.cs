namespace HatuxDevLab.Web.Services;

public class GreetingService
{
    public string CreateGreeting(string name)
    {
        return $"Hello {name}";
    }
}