using System;
using UnityEngine;

public class UnderMap : MonoBehaviour
{
    [SerializeField] private GameManager GameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.hearts -= 20;
        }
    }
}
