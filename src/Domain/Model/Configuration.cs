namespace Domain.Model;

public class Configuration
{
    public string? Url {get; private set;}
    public string? Username {get; private set;}
    public string? Password {get; private set;}
    public Configuration(ConfigurationBuilder builder)
    {
        Url = builder.Url;
        Username = builder.Username;
        Password = builder.Password;
    }

}