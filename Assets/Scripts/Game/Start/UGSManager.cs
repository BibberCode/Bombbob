using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class UGSManager : MonoBehaviour
{
    public static Task InitializationTask;
    public static string PlayerName = ""; // globaler Name für PlayerInfo etc.

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Nur einmal initialisieren
        if (InitializationTask == null)
        {
            InitializationTask = InitializeUGS();
        }
    }

    private async Task InitializeUGS()
    {
        Debug.Log("UGS: Initialisierung startet...");

        // Unity Services starten
        await UnityServices.InitializeAsync();

        // Falls noch nicht eingeloggt → anonym anmelden
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch
            {
                // Falls der gespeicherte Token kaputt ist → löschen und neu anmelden
                AuthenticationService.Instance.ClearSessionToken();
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        Debug.Log("UGS: Erfolgreich initialisiert!");
        Debug.Log("PlayerID: " + AuthenticationService.Instance.PlayerId);
    }
}