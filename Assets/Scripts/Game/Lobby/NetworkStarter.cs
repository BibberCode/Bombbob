using Unity.Netcode;
using UnityEngine;

public class NetworkStarter : MonoBehaviour
{
    private void Start()
    {
        if (NetworkManager.Singleton == null)
        {
            return;
        }

        if (NetworkStartConfig.StartAsHost && !NetworkManager.Singleton.IsHost && !NetworkManager.Singleton.IsClient)
        {
            bool success = NetworkManager.Singleton.StartHost();
        }
        else if (NetworkStartConfig.StartAsClient && !NetworkManager.Singleton.IsHost && !NetworkManager.Singleton.IsClient)
        {
            bool success = NetworkManager.Singleton.StartClient();
        }
    }
}