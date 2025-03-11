using System;
using System.Collections.Generic;
using UnityEngine;

public static class KickManager
{
   
    private static INetworkManager networkManager = NetworkManager.Instance;

    public static void KickPlayer(string playerId)
    {
        if (networkManager == null || !IsHost())
            return;
        
        networkManager.KickPlayer(playerId);
    }

    public static bool IsHost()
    {
        
        return networkManager.IsHost;
    }
}
