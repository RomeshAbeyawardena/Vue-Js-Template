namespace PackageManager.Shared.Abstractions
{
    public interface IConsoleHost
    {
        string Key { get; set; }
        bool Default { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        string Arguments { get; set; }
    }
}
