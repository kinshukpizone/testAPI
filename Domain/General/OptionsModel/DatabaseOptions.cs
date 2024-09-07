namespace Domain.General.OptionsModel
{
    public class DatabaseOptions
    {
        public string ConnectionStrings { get; set; } = string.Empty;
        public int MaxRetryCount { get; set; }
        public int CommandTimeout { get; set; }
        public int CommandTimeoutForAutoMigrate { get; set; }
        public bool EnableDetailedErrors { get; set; }
        public bool EnableSensitiveDataLoggin { get; set; }
    }
}
