using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMessage : MonoBehaviour
{
    [SerializeField] private GameObject playerCamPrefab;
    private GameObject player;
    [SerializeField] private GameObject deadMessageCanvas;

    private void Start()
    {
        if (PlayerNetworkManager.OwnerIs)
        {
            player = GameObject.Find("Player(Clone)");
        }
    }

    private void Update()
    {
        if (!PlayerNetworkManager.OwnerIs) { return; }
        if (SceneManager.GetActiveScene().name != "Game") { return; }

        if (GameManager.instance.hearts <= 0)
        {
                Instantiate(playerCamPrefab, player.transform.position, player.transform.rotation);
                deadMessageCanvas.SetActive(true);
                Destroy(player);
                gameObject.GetComponent<DeadMessage>().enabled = false;
        }
    }
}
