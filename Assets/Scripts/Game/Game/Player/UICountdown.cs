using TMPro;
using UnityEngine;
using Unity.Netcode;

public class UICountdown : NetworkBehaviour
{
    private TextMeshProUGUI text;
    public float beginTimer = 5;

    private bool startCountdown = false;

    // 👇 NEU: globaler Start-Status
    public NetworkVariable<bool> gameStarted = new NetworkVariable<bool>(false);

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!startCountdown) return;

        beginTimer -= Time.deltaTime;
        text.text = beginTimer.ToString("F0");

        if (beginTimer <= 0)
        {
            gameObject.SetActive(false);

            if (IsServer)
            {
                gameStarted.Value = true; // 👈 alle bekommen das
            }
        }
    }

    [ClientRpc]
    public void StartCountdownClientRpc()
    {
        startCountdown = true;
    }
}