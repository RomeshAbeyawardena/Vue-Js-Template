namespace PackageManager.Shared.Abstractions
{
    public interface IConfigurationLoader
    {
        IConfiguration LoadConfigurationFromXml(IConfiguration configuration,
            string defaultConfigurationPath = default);
    }
}
