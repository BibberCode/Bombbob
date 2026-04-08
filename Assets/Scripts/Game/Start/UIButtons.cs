using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UIButtons : MonoBehaviour
{
    public GameObject loadingCanvas;
    public VideoPlayer videoPlayer;

    public void OnHostButtonClicked()
    {
        NetworkStartConfig.StartAsHost = true;
        NetworkStartConfig.StartAsClient = false;

        loadingCanvas.SetActive(true);
        StartCoroutine(LoadingNextScene());
    }

    public void OnClientButtonClicked()
    {
        NetworkStartConfig.StartAsHost = false;
        NetworkStartConfig.StartAsClient = true;

        loadingCanvas.SetActive(true);
        StartCoroutine(LoadingNextScene());
    }


    IEnumerator LoadingNextScene()
    {
        // 1. Video vorbereiten
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null; // Warten bis das Video bereit ist
        }

        // 2. Video abspielen
        videoPlayer.Play();

        // 3. Szene laden, aber noch nicht aktivieren
        AsyncOperation async = SceneManager.LoadSceneAsync("Lobby");
        async.allowSceneActivation = false;

        // 4. Optional: Wartezeit für Video (z. B. 2 Sekunden)
        yield return new WaitForSeconds(2f);

        // 5. Szene aktivieren
        async.allowSceneActivation = true;
    }
}
