using BepInEx;
using HarmonyLib;
using REPOConfig;

[BepInPlugin("com.TankDaDank.RepKickMod", "R.E.P.0 Kick Mod", "1.3.0")]
public class REPOKickMod : BaseUnityPlugin
{
    private void Awake()
    {
        ConfigManager.LoadConfig();
        Harmony harmony = new Harmony(com.TankDaDank.RepKickMod");
        harmony.PatchAll();
    }
}
