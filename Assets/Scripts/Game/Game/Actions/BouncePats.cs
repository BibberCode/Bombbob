using UnityEngine;

public class BouncePats : MonoBehaviour
{
    [SerializeField] private float bounceForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 bounce = Vector3.up * bounceForce; // 5f ist die Stärke – kannst du anpassen
            collision.rigidbody.AddForce(bounce, ForceMode.Impulse); // Impulse für sofortigen Effekt
        }
    }
}
