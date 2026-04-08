using UnityEngine;
using Unity.Netcode;

public class PlayerLook : NetworkBehaviour
{
    [SerializeField] private float mouseSensitivity = 5f;
    [SerializeField] private Transform playerCamera;

    private PlayerControls controls;
    private Vector2 lookInput;
    private float xRotation = 0f;

    public override void OnNetworkSpawn()
    {
        if (!IsLocalPlayer)
        {
            enabled = false;
            return;
        }

        controls = new PlayerControls();
        controls.Enable();

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Disable();
        }
    }

    private void Update()
    {
        if (Application.isMobilePlatform) return;

        lookInput = controls.Player.Look.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Vertikale Rotation der Kamera (Pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontale Rotation des Spielers (Yaw)
        transform.Rotate(Vector3.up * mouseX);
    }
}