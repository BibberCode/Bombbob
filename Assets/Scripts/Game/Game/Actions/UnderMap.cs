using System;
using UnityEngine;

public class UnderMap : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthDeath>()?.TakeDamage(2f);
        }
    }
}
