using UnityEngine;

public class Teleports : MonoBehaviour
{
    public Transform spawnTeleport1, spawnTeleport2;

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name == "Teleport1")
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.transform.position = spawnTeleport2.transform.position;
            }
        }

        if (gameObject.name == "Teleport2")
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.transform.position = spawnTeleport1.transform.position;
            }
        }
    }
}
