using UnityEngine;
using UnityEngine.InputSystem;

public class UniversallKeys : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Keyboard.current.f11Key.wasPressedThisFrame)
        {
            if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
            {
                // Fenster-Modus (z.B. 1280x720 oder letzte Fenstergröße)
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
            }
            else
            {
                // Minecraft-Style Fullscreen → IMMER native Auflösung setzen
                Resolution res = Screen.currentResolution;
                Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);
            }
        }
    }
}