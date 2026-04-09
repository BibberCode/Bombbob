using UnityEngine;
using Unity.Netcode;

public class ExplosionLifetime : NetworkBehaviour
{
    [SerializeField] private float lifeTime;

    private void Update()
    {
        if (!IsOwner)
        lifeTime -= Time.deltaTime;

        if ( lifeTime < 0)
            Destroy(gameObject);
    }
}
