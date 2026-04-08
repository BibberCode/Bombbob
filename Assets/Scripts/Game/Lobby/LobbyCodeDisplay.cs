using TMPro;
using UnityEngine;

public class LobbyCodeDisplay : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SetTextDelayed());
    }

    private System.Collections.IEnumerator SetTextDelayed()
    {
        yield return new WaitForSeconds(1.5f); // kurze Verzögerung
        GetComponent<TMP_Text>().text = "LobbyCode: " + MultiplayerManager.Instance.JoinCode;
    }
}
