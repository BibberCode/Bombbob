using UnityEngine;

public class UnderMap : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.hearts -= 20;
        }
    }
}
