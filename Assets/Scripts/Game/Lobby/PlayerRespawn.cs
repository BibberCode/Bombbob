using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform playerSpawn;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = playerSpawn.position;
        }
    }
    
}
