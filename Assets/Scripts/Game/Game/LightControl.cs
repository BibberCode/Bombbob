using UnityEngine;

public class LightControl : MonoBehaviour
{
    [SerializeField] private Light globalGameLight;
    private float lightIntensity = 1;
    private bool morning;
    [SerializeField] private float daySpeed = 0.1f;

    private void Update()
    {
        if (morning) { Morning(); }
        else { Evening(); }

        globalGameLight.intensity = lightIntensity;
    }

    private void Morning()
    {
        lightIntensity += Time.deltaTime * daySpeed;

        if (lightIntensity >=2) { morning = false; }
    }

    private void Evening()
    {
        lightIntensity -= Time.deltaTime * daySpeed;

        if (lightIntensity <= 0.2) { morning = true; }
    }
}
