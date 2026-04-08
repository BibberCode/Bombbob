using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementPhysics : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform orientation;

    private PlayerControls controls;
    private Vector2 moveInput;
    private bool jumpPressed;
    public bool isGrounded = false;

    private Rigidbody rb;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx => jumpPressed = true;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // verhindert ungewolltes Kippen
    }

    void FixedUpdate()
    {  
        Vector3 moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPressed = false;
            isGrounded = false;
        }

        if (Keyboard.current.ctrlKey.isPressed) { moveSpeed = 8; }
        else { moveSpeed = 5f;  }
    }
}