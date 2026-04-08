using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UniversalButtons : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
}
