namespace PackageManager.Shared.Domain.Models
{
    public class Module
    {
        public bool Default { get; set; }
        public bool Enabled { get; set; }
        public string Assembly { get; set; }

        public string Type { get; set; }
    }
}
