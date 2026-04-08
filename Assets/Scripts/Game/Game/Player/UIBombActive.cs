using UnityEngine;
using Unity.Netcode;
using System.Collections;
using System.Threading;

public class UIBombActive : MonoBehaviour
{
    public GameObject ActiveYes;
    public GameObject ActiveNo;
    public GameObject FullChaged;

    private BombThrow bombThrow;
    private bool go = false;

    private void Start()
    {
        StartCoroutine(DelayRoutine());
    }

    IEnumerator DelayRoutine()
    {
        yield return new WaitForSeconds(1f);
        go = true;
    }


    void Update()
    {
        if (!go)
            return;

        if (bombThrow == null)
            bombThrow = FindLocalPlayerBombThrow();

        if (bombThrow == null)
            return;

        if (bombThrow.timer == 1)
        {
            ActiveYes.SetActive(true);
            ActiveNo.SetActive(false);
            FullChaged.SetActive(false);
        }
        else
        {
            ActiveYes.SetActive(false);
            ActiveNo.SetActive(true);
            FullChaged.SetActive(false);
        }

        // Voll aufgeladen?
        if (bombThrow.currentPower >= 25f)
        {
            ActiveYes.SetActive(false);
            ActiveNo.SetActive(false);
            FullChaged.SetActive(true);
        }
        else
        {
            FullChaged.SetActive(false);
        }
    }

    private BombThrow FindLocalPlayerBombThrow()
    {
        foreach (var bt in FindObjectsByType<BombThrow>(FindObjectsInactive.Include))
        {
            if (bt.IsOwner)
                return bt;
        }

        return null;
    }
}