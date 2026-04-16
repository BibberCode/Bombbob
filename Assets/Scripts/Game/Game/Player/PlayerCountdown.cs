using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerCountdown : NetworkBehaviour
{
    private PlayerMovementPhysics movement;
    private BombThrow bomb;
    private UICountdown countdown;

    private bool initialized = false;

    public override void OnNetworkSpawn()
    {
        movement = GetComponent<PlayerMovementPhysics>();
        bomb = GetComponent<BombThrow>();

        if (IsServer)
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(string sceneName, LoadSceneMode mode,
        List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if (sceneName != "Game") return;

        Debug.Log("Game Scene geladen → Player deaktivieren");

        // Movement deaktivieren
        if (movement != null) movement.enabled = false;
        if (bomb != null) bomb.enabled = false;

        initialized = false; // neu suchen in neuer Scene
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Game") return;

        // Countdown finden (nach Scene-Wechsel)
        if (!initialized)
        {
            countdown = FindFirstObjectByType<UICountdown>();

            if (countdown != null)
            {
                initialized = true;
            }
            return;
        }

        if (countdown == null) return;

        // Wenn Countdown fertig → aktivieren
        if (countdown.gameStarted.Value)
        {
            if (movement != null && !movement.enabled)
                movement.enabled = true;

            if (bomb != null && !bomb.enabled)
                bomb.enabled = true;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (NetworkManager.Singleton != null && IsServer)
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted -= OnSceneLoaded;
        }
    }
}