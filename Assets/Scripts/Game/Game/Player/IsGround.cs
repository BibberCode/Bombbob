using UnityEngine;

public class IsGround : MonoBehaviour
{
    public PlayerMovementPhysics Physics;

    private void OnTriggerEnter()
    {
        Physics.isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.isGrounded = false;
    }
}
