namespace Data.Repository;

using IniParser;
using IniParser.Model;

using Domain.Model;
using Domain.Repository;

public class IniConfigurationRepository: IConfigurationRepository
{
    private string path;
    
    public IniConfigurationRepository(string path)
    {
        this.path = path;
    }

    public Configuration Load()
    {
        var parser = new FileIniDataParser();
        IniData data = parser.ReadFile(path);

        string? username = null;
        string? password = null;
        string? url = null;

        if(data["login"].ContainsKey("password"))
        {
            password = data["login"]["password"];
        }
        
        if(data["login"].ContainsKey("username"))
        {
            username = data["login"]["username"];
        }
        
        if(data["login"].ContainsKey("url"))
        {
            url = data["login"]["url"];
        }

        return new ConfigurationBuilder()
            .WithPassword(password)
            .WithUrl(url)
            .WithUsername(username)
            .Build();
    }

    public void Save(Configuration config)
    {
        var parser = new FileIniDataParser();
        IniData data = parser.ReadFile(path);

        if(config.Username != null)
        {
            data["login"]["username"] = config.Username;
        }
        
        if(config.Password != null)
        {
            data["login"]["password"] = config.Password;
        }
        
        if(config.Url != null)
        {
            data["login"]["url"] = config.Url;
        }
    }
}