using UnityEngine;
using UnityEngine.InputSystem; // wichtig!

public class UniversallKeys : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Escape zum Beenden
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }

        // F11 für Vollbild
        if (Keyboard.current.f11Key.wasPressedThisFrame)
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}