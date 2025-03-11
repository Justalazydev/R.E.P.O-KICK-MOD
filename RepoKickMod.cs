using BepInEx;
using HarmonyLib;
using REPOConfig;
using UnityEngine;
using UnityEngine.SceneManagement;

[BepInPlugin("com.TankDaDank.REPOKickMod", "REPO Kick Mod", "1.3.0")]
public class REPOKickMod : BaseUnityPlugin
{
    private void Awake()
    {
        ConfigManager.LoadConfig();
        Harmony harmony = new Harmony("com.TankDaDank.REPOKickMod");
        harmony.PatchAll();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MultiplayerLobby")
        {
            if (KickManager.IsHost() && ConfigManager.IsKickEnabled)
            {
                UIManager.InitializeUI();
            }
        }
        else
        {
            UIManager.ClearUI();
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
