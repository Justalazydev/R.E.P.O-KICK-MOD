using REPOConfig;

public static class ConfigManager
{

    public static bool IsKickEnabled { get; private set; }
    public static bool RequireConfirmation { get; private set; }

    public static void LoadConfig()
    {
        IsKickEnabled = REPOConfigManager.GetBool("KickEnabled", true);
        RequireConfirmation = REPOConfigManager.GetBool("RequireConfirmation", true);
    }
}
