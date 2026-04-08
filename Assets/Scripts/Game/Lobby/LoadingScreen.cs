using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadingScreen : NetworkBehaviour
{
    public GameObject loadingCanvas;
    public string zielSzene = "Game";

    private bool isReady = false;

    public override void OnNetworkSpawn()
    {
        StartCoroutine(WaitForServerReady());
    }

    private IEnumerator WaitForServerReady()
    {
        // Warte, bis NGO vollständig initialisiert ist
        while (!IsServer)
            yield return null;

        isReady = true;
    }

    private void Update()
    {
        if (!isReady) return; // erst wenn Netcode bereit ist

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            loadingCanvas.SetActive(true);

            NetworkManager.Singleton.SceneManager.LoadScene(
                zielSzene,
                LoadSceneMode.Single
            );
        }
    }
}