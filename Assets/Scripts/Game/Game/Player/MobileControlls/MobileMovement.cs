using TouchControlsKit;
using UnityEngine;

public class MobileMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private Transform playerCamera;
    private float xRotation = 0f;

    private void FixedUpdate()
    {
        if (!Application.isMobilePlatform) return;

        Vector2 move = TCKInput.GetAxis("JoystickMove");

        Vector3 moveDirection = transform.forward * move.y;
        moveDirection += transform.right * move.x;
    }

    private void Update()
    {
        if (!Application.isMobilePlatform) return;

        Vector2 look = TCKInput.GetAxis("Touchpadlook");

        float mouseX = look.x;
        float mouseY = look.y;

        // Vertikale Rotation der Kamera (Pitch)
        xRotation -= look.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontale Rotation des Spielers (Yaw)
        transform.Rotate(Vector3.up * mouseX);
    }
}
