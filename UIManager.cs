using UnityEngine;
using MenuLib;
using System.Collections.Generic;
using System;

public static class UIManager
{
    private static MenuPanel kickMenuPanel;

    public static void InitializeUI()
    {
        ClearUI();
        kickMenuPanel = new MenuPanel("Kick Menu");
        List<PlayerInfo> players = KickManager.GetPlayerList();
        foreach (PlayerInfo player in players)
        {
            if (!IsLocalPlayer(player))
            {
                kickMenuPanel.AddButton(new MenuButton($"Kick {player.PlayerName}", () => OnKickButtonClicked(player)));
            }
        }
        MenuUI.AddPanel(kickMenuPanel);
    }

    public static void ClearUI()
    {
        if (kickMenuPanel != null)
        {
            MenuUI.RemovePanel(kickMenuPanel);
            kickMenuPanel = null;
        }
    }

    private static void OnKickButtonClicked(PlayerInfo player)
    {
        if (ConfigManager.RequireConfirmation)
        {
            ShowConfirmationDialog(player.PlayerName, (confirmed) =>
            {
                if (confirmed)
                {
                    KickManager.KickPlayer(player.PlayerId);
                    InitializeUI();
                }
            });
        }
        else
        {
            KickManager.KickPlayer(player.PlayerId);
            InitializeUI();
        }
    }

    private static bool IsLocalPlayer(PlayerInfo player)
    {
        string localPlayerId = "HostPlayer";
        return player.PlayerId == localPlayerId;
    }

    private static void ShowConfirmationDialog(string playerName, Action<bool> callback)
    {
        Debug.Log($"Confirm kicking player: {playerName}?");
        callback.Invoke(true);
    }
}
