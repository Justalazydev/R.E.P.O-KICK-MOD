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
        int count = Math.Min(players.Count, 20);
        for (int i = 0; i < count; i++)
        {
            PlayerInfo player = players[i];
            if (!IsLocalPlayer(player))
            {
                REPOButton kickButton = null;
                kickButton = new REPOButton("Kick " + player.PlayerName, () =>
                {
                    if (ConfigManager.RequireConfirmation)
                    {
                        kickButton.OpenDialog("Confirmation", "Are you sure you want to kick " + player.PlayerName + "?", () =>
                        {
                            KickManager.KickPlayer(player.PlayerId);
                            InitializeUI();
                        });
                    }
                    else
                    {
                        KickManager.KickPlayer(player.PlayerId);
                        InitializeUI();
                    }
                });
                kickMenuPanel.AddElement(kickButton);
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
    private static bool IsLocalPlayer(PlayerInfo player)
    {
        return player.PlayerName == "host name";
    }
}
