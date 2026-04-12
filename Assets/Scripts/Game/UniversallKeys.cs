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
        // if (Keyboard.current.escapeKey.wasPressedThisFrame)
        // {
        //     Application.Quit();
        // }

        if (Keyboard.current.f11Key.wasPressedThisFrame)
{
        if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
        {
            // → Minecraft-Style (randloses Fenster mit Taskleiste)
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        }
        else
        {
            // → echtes Vollbild
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
}
    }
}