namespace Domain.Model;

public class ConfigurationBuilder
{
    public string? Url {get; private set;}
    public string? Username {get; private set;}
    public string? Password {get; private set;}

    public ConfigurationBuilder() {}
    public ConfigurationBuilder(string url, string username, string password)
    {
        Url = url;
        Username = username;
        Password = password;
    }

    public ConfigurationBuilder WithUrl(string? url)
    {
        Url = url;
        return this;
    }
    
    public ConfigurationBuilder WithUsername(string? username)
    {
        Username = username;
        return this;
    }
    
    public ConfigurationBuilder WithPassword(string? password)
    {
        Password = password;
        return this;
    }

    public Configuration Build()
    {
        return new Configuration(this);
    }
    
}