using System.Collections.Generic;
using TMPro;
using Unity.Services.CloudSave;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Names : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    private string playerName;

    private async void Start()
    {
        await UGSManager.InitializationTask;

        var result = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "player_name" });
        if (result.TryGetValue("player_name", out var cloudValue))
        {
            playerName = cloudValue;
            nameInput.text = playerName;
        }
        else
        {
            playerName = "Nobody";
            nameInput.text = "";
        }

        MultiplayerManager.Instance.PlayerName = playerName; // ← Übergabe an LobbyManager
    }

    public async void OnNameChanged(string newName)
    {
        playerName = string.IsNullOrEmpty(newName) ? "Nobody" : newName;
        await SaveNameToCloud(playerName);
        MultiplayerManager.Instance.PlayerName = playerName;
    }

    public void StartClientButtonPressed()
    {
        if (MultiplayerManager.Instance.joinSuccess)
        {
            GameStart();
        }
        else
        {
            Debug.Log("Start nicht möglich – Join war nicht erfolgreich.");
        }
    }

    public async void GameStart()
    {
        playerName = string.IsNullOrEmpty(nameInput.text) ? "Nobody" : nameInput.text;
        await SaveNameToCloud(playerName);
        MultiplayerManager.Instance.PlayerName = playerName;
        SceneManager.LoadScene("Lobby");
    }

    private async System.Threading.Tasks.Task SaveNameToCloud(string name)
    {
        var data = new Dictionary<string, object> { { "player_name", name } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        Debug.Log("Name gespeichert: " + name);
    }
}