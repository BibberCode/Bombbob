using System;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;


public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    public string PlayerName = "Nobody";
    public string JoinCode;
    public bool IsHost;
    [SerializeField] private TMP_InputField joinCodeInput;
    public bool joinSuccess = false;

    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private GameObject wrongCode;

    private async void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Warten auf UGSManager-Initialisierung
            await UGSManager.InitializationTask;

            // Anonyme Anmeldung, falls noch nicht erfolgt
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
            }
        }
        else
        {
            Destroy(gameObject);
        }

        wrongCode.SetActive(false);
    }

    public async void StartHost()
    {
        loadingCanvas.SetActive(true);

        try
        {
            IsHost = true;

            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4);
            JoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("Join Code: " + JoinCode);

            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException e)
        {
            Debug.LogError("Host-Fehler: " + e.Message);
        }
    }

    public void StartClient()
    {
        string code = joinCodeInput.text;
        JoinLobby(code);
    }

    public async void JoinLobby(string joinCode)
    {
        joinSuccess = false;

        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
            );

            loadingCanvas.SetActive(true);

            NetworkManager.Singleton.StartClient();
            Debug.Log("Client gestartet mit Join Code: " + joinCode);

            joinSuccess = true;
        }
        catch (Exception e)
        {
            Debug.LogError("Fehler beim Joinen: " + e.Message);
            joinSuccess = false;
            wrongCode.SetActive(true);
        }
    }

    public void JoinCodeInputChanges()
    {
        wrongCode.SetActive(false);
    }
}