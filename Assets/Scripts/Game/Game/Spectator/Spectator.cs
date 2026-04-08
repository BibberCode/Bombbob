using UnityEngine;
using UnityEngine.InputSystem;

public class Spectator : MonoBehaviour
{
    [Header("Look")]

    [SerializeField] private float mouseSensitivity = 10f, moveSpeed = 30;
    [SerializeField] private Transform playerCamera;

    private PlayerControls lookControls;
    private Vector2 lookInput;
    private float xRotation = 0f;

    void Awake()
    {   
        if (!PlayerNetworkManager.OwnerIs) { gameObject.SetActive(false); }

        //Look
        lookControls = new PlayerControls();
        lookControls.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    //Look
    private void Update()
    {
        lookInput = lookControls.Player.Look.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Vertikale Rotation der Kamera (Pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontale Rotation des Spielers (Yaw)
        transform.Rotate(Vector3.up * mouseX);


        Movement();
    }

    private void OnDisable()
    {
        if (lookControls != null)
        {
            lookControls.Disable();
        }
    }

    private void Movement()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            Vector3 upVetor = Vector3.up;
            transform.Translate(upVetor * moveSpeed * Time.deltaTime);
        }

        if (Keyboard.current.leftShiftKey.isPressed)
        {
            Vector3 downVetor = -Vector3.up;
            transform.Translate(downVetor * moveSpeed * Time.deltaTime);
        }

        if (Keyboard.current.wKey.isPressed)
        {
            Vector3 forwardVetor = Vector3.forward;
            transform.Translate(forwardVetor * moveSpeed * Time.deltaTime);
        }

        if (Keyboard.current.aKey.isPressed)
        {
            Vector3 leftVetor = -Vector3.right;
            transform.Translate(leftVetor * moveSpeed * Time.deltaTime);
        }

        if (Keyboard.current.sKey.isPressed)
        {
            Vector3 backVetor = -Vector3.forward;
            transform.Translate(backVetor * moveSpeed * Time.deltaTime);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            Vector3 rightVetor = Vector3.right;
            transform.Translate(rightVetor * moveSpeed * Time.deltaTime);
        }
    }
}
