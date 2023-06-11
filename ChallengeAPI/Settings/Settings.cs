namespace ChallengeAPI.Settings
{
    public static class Settings
    {
        public static string Secret = "fedaf7d8863b48e197b9287d492b708e";

        public static string? _azureBusConnection = Environment.GetEnvironmentVariable("AZURE_SERVICE_BUS_CONNECTION");
    }
}
