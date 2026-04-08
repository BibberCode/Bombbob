using UnityEngine;

public class BombBlinking : MonoBehaviour
{
    public float explosionTime = 3f; // Zeit bis zur Explosion

    private Renderer bombRenderer;
    private bool isRed = false;
    private float blinkTimer = 0f;
    private float timeElapsed = 0f;

    void Start()
    {
        bombRenderer = GetComponent<Renderer>();
        bombRenderer.material.color = Color.black;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Blinkgeschwindigkeit steigt exponentiell mit der Zeit
        float blinkInterval = Mathf.Lerp(0.5f, 0.05f, timeElapsed / explosionTime);
        blinkTimer += Time.deltaTime;

        if (blinkTimer >= blinkInterval)
        {
            ToggleColor();
            blinkTimer = 0f;
        }
    }

    void ToggleColor()
    {
        bombRenderer.material.color = isRed ? Color.black : Color.red;
        isRed = !isRed;
    }
}