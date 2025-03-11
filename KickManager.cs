using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class KickManager
{
    private static INetworkManager networkManager = NetworkManager.Instance;
    public static void KickPlayer(string playerId)
    {
        if (!IsHost())
        {
            Debug.Log("Only the host can kick players.");
            return;
        }
        if (networkManager == null)
        {
            Debug.LogError("NetworkManager instance not found!");
            return;
        }
        Debug.Log($"Kicking player: {playerId}");
        networkManager.KickPlayer(playerId);
    }
    public static bool IsHost()
    {
        return networkManager != null && networkManager.IsHost;
    }
    public static List<PlayerInfo> GetPlayerList()
    {
        if (networkManager != null)
        {
            return networkManager.GetPlayers().Take(6).ToList();
        }
        return new List<PlayerInfo>();
    }
}

public interface INetworkManager
{
    bool IsHost { get; }
    void KickPlayer(string playerId);
    List<PlayerInfo> GetPlayers();
}

public class NetworkManager : INetworkManager
{
    private static NetworkManager _instance;
    public static NetworkManager Instance 
    { 
        get 
        {
            if (_instance == null)
                _instance = new NetworkManager();
            return _instance;
        }
    }
    public bool IsHost { get; private set; } = true;
    public void KickPlayer(string playerId)
    {
        Debug.Log($"[NetworkManager] Player {playerId} has been kicked.");
    }
    public List<PlayerInfo> GetPlayers()
    {
        return new List<PlayerInfo>
        {
            new PlayerInfo { PlayerId = "HostPlayer", PlayerName = "host name" },
            new PlayerInfo { PlayerId = "Player1", PlayerName = "Player1" },
            new PlayerInfo { PlayerId = "Player2", PlayerName = "Player2" },
            new PlayerInfo { PlayerId = "Player3", PlayerName = "Player3" },
            new PlayerInfo { PlayerId = "Player4", PlayerName = "Player4" },
            new PlayerInfo { PlayerId = "Player5", PlayerName = "Player5" }
        };
    }
}

public class PlayerInfo
{
    public string PlayerId;
    public string PlayerName;
}
