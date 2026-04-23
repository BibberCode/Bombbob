using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthDeath : MonoBehaviour
{
    [Header("Health")]
    public float hearts = 10f;

    [Header("Death")]
    [SerializeField] private GameObject spectatorPrefab;
    [SerializeField] private GameObject deadMessageCanvas;

    private bool isDead;

    private void Update()
    {
        if (!PlayerNetworkManager.OwnerIs) return;
        if (SceneManager.GetActiveScene().name != "Game") return;
        if (isDead) return;

        if (hearts <= 0f)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        hearts -= damage;

        if (hearts <= 0f)
        {
            hearts = 0f;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        Instantiate(spectatorPrefab, transform.position, transform.rotation);

        if (deadMessageCanvas != null)
            deadMessageCanvas.SetActive(true);

        Destroy(gameObject);
        enabled = false;
    }
}