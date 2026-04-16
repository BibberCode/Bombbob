using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameStartManager : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
    }

    private void OnSceneLoaded(string sceneName, LoadSceneMode mode,
        List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        // Event wieder entfernen (wichtig!)
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted -= OnSceneLoaded;

        Debug.Log("Alle Spieler sind in der Scene!");

        // Countdown starten
        StartCountdown();
    }

    void StartCountdown()
    {
        UICountdown countdown = FindFirstObjectByType<UICountdown>();

        if (countdown != null)
        {
            countdown.StartCountdownClientRpc();
        }
    }
}