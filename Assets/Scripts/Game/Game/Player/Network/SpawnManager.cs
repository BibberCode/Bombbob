using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (spawnPoints.Count > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Count);
                Transform selectedSpawn = spawnPoints[randomIndex];

                // Objekt verwenden (z. B. Spieler dort platzieren)
                other.gameObject.transform.position = selectedSpawn.position;

                // Danach aus der Liste entfernen
                spawnPoints.RemoveAt(randomIndex);
            }
            else
            {
                Debug.LogWarning("Keine Spawnpunkte mehr verfügbar!");
            }

        }
    }
}