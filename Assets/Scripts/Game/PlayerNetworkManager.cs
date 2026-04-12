using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNetworkManager : NetworkBehaviour
{
    Material material;
    MeshRenderer colorRenderer;
    NetworkVariable<Color> color = new NetworkVariable<Color>(writePerm: NetworkVariableWritePermission.Owner);
    List<Color> colorList = new List<Color>()
    {
        Color.blue,
        Color.green,
        Color.red,
    };

    [SerializeField] private GameObject playerCamera;

    public static bool OwnerIs = false;

    private Rigidbody rb;
    private bool isKnockback = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnNetworkSpawn()
    {
        colorRenderer = GetComponent<MeshRenderer>();
        material = colorRenderer.material;

        int randomIndex = Random.Range(0, colorList.Count);

        color.OnValueChanged += OnColorChanged;
        color.Value = colorList[randomIndex];

        if (IsOwner)
        {
            OwnerIs = true;

            // Farbe setzen
            color.Value = colorList[randomIndex];

            // Lokale Komponenten aktivieren
            gameObject.GetComponent<BombThrow>().enabled = true;
            gameObject.GetComponent<PlayerLook>().enabled = true;
            gameObject.GetComponent<PlayerMovementPhysics>().enabled = true;

            // Kamera aktivieren
            playerCamera.SetActive(true);
        }
        else
        {
            OwnerIs = false;

            // Fremde Spieler deaktivieren
            gameObject.GetComponent<BombThrow>().enabled = false;
            gameObject.GetComponent<PlayerLook>().enabled = false;
            gameObject.GetComponent<PlayerMovementPhysics>().enabled = false;

            // Kamera deaktivieren
            playerCamera.SetActive(false);
        }
    }

    private void OnColorChanged(Color previousColor, Color newColor)
    {
        if (material != null)
        {
            material.color = newColor;
        }
    }

    private void Start()
    {
        material.color = color.Value;

        gameObject.GetComponent<PlayerNetworkManager>().enabled = true;
    }

    [ClientRpc]
    public void ApplyKnockbackClientRpc(Vector3 force, ClientRpcParams rpcParams = default)
    {
        if (!IsOwner) return;

        if (rb != null)
        {
            isKnockback = true;
            rb.AddForce(force, ForceMode.Impulse);
            StartCoroutine(ResetKnockback());
        }
    }

    IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.2f);
        isKnockback = false;
    }

    private void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            int randomIndex = Random.Range(0, colorList.Count);
            color.Value = colorList[randomIndex];
        }
    }
}
