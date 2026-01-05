namespace Domain.Repository;

using Domain.Model;

public interface IConfigurationRepository
{
    Configuration Load();
    void Save(Configuration config);
}