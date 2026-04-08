using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombExplosion : NetworkBehaviour
{
    private float bigger = 1f;

    [SerializeField] private GameObject visualExplosionPrefab;

    private void Update()
    {
        if (!IsServer) return;

        // Explosion wächst nur auf dem Server (Gameplay)
        bigger += Time.deltaTime * 50f;
        transform.localScale = Vector3.one * bigger;

        if (bigger >= 6f)
        {
            // Visuelle Explosion auf allen Clients abspielen
            PlayVisualExplosionClientRpc(transform.position);

            // Gameplay-Explosion ist vorbei → Bombe entfernen
            GetComponent<NetworkObject>().Despawn(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                GameManager.instance.hearts -= 0.5f;
            }
        }
    }

    // VISUELLE Explosion (nur Grafik, kein Gameplay)
    [ClientRpc]
    private void PlayVisualExplosionClientRpc(Vector3 pos)
    {
        Instantiate(visualExplosionPrefab, pos, Quaternion.identity);
    }
}