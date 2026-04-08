using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICountdown : MonoBehaviour
{
    public static UICountdown instance;

    private TextMeshProUGUI TMPro;
    public float beginTimer = 5;

    void Start()
    {
        instance = this;
        TMPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            beginTimer -= Time.deltaTime;
            TMPro.text = beginTimer.ToString("F0");
        }

        if (beginTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
