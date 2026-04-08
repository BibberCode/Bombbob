using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNameDisplay : NetworkBehaviour
{
    private TextMeshPro nameText;

    // Jeder Spieler hat seine eigene NetworkVariable
    private NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>(
        writePerm: NetworkVariableWritePermission.Owner
    );

    public override void OnNetworkSpawn()
    {
        nameText = GetComponent<TextMeshPro>();

        // Wenn ich der Owner bin → meinen Namen setzen
        if (IsOwner)
        {
            playerName.Value = MultiplayerManager.Instance.PlayerName;
        }

        // Wenn sich der Name ändert → Text aktualisieren
        playerName.OnValueChanged += OnNameChanged;

        // Direkt initial setzen
        nameText.text = playerName.Value.ToString();
    }

    private void OnNameChanged(FixedString32Bytes oldName, FixedString32Bytes newName)
    {
        nameText.text = newName.ToString();
    }
}