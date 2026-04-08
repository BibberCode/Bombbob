using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class BombForce : NetworkBehaviour
{
    private Rigidbody rb;
    public float forceStrength = 5f;
    private float timer = 3f;
    private bool instantExplosion = false;

    public GameObject bombExplosion;
    public Transform bomb;

    public BombThrow bombThrow;   // Referenz auf das Script

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody>();

        if (IsServer)
        {
            rb.isKinematic = false;   // Server simuliert Physik
        }
        else
        {
            rb.isKinematic = true;    // Client NICHT
        }
    }

    public void ApplyForce()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * forceStrength, ForceMode.Impulse);
        StartCoroutine(ExplosionTimer());
    }

    private IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(timer);

        if (!instantExplosion)
            Explode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) return;

        if (forceStrength >= 25)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (instantExplosion)
            return;

        instantExplosion = true;

        GameObject explosion = Instantiate(bombExplosion, bomb.position, bomb.rotation);
        explosion.GetComponent<NetworkObject>().Spawn(true);

        GetComponent<NetworkObject>().Despawn(true);
    }
}