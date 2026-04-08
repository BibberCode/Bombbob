using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCountdown : MonoBehaviour
{
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (UICountdown.instance.beginTimer <= 0.01f)
            {
                gameObject.GetComponent<PlayerMovementPhysics>().enabled = true;
                gameObject.GetComponent<BombThrow>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<PlayerMovementPhysics>().enabled = false;
                gameObject.GetComponent<BombThrow>().enabled = false;
            }
        }
        
    }
}
