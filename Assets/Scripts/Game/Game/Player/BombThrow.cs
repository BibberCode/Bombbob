using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using TouchControlsKit;

public class BombThrow : NetworkBehaviour
{
    public GameObject bomb;
    public GameObject bombThrowPosition;

    public float timer;
    private float ogTimer = 1f;
    private bool timerThrow = true;

    private Transform throwAim;

    // POWER
    public float currentPower = 0f;
    public float maxPower = 50f;
    public float minPower = 3f;
    public float chargeRate = 4f;

    private void Start()
    {
        currentPower = minPower;
        timer = ogTimer;  // Timer beim Start auf 1f setzen!
        throwAim = bombThrowPosition.transform;
    }

    private void Update()
    {
        Timer();

        // Power aufladen
        if (Mouse.current.leftButton.isPressed || TCKInput.GetAction("fireBtn", EActionEvent.Down))
        {
            currentPower += chargeRate * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, minPower, maxPower);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && timer == ogTimer || TCKInput.GetAction("fireBtn", EActionEvent.Up) && timer == ogTimer)
        {
            RequestThrowServerRpc(currentPower);
            Throwtimer();
            currentPower = minPower;
        }
    }

    // CLIENT → SERVER
    [ServerRpc]
    private void RequestThrowServerRpc(float power)
    {
        // Bombe erzeugen
        GameObject b = Instantiate(bomb, throwAim.position, bombThrowPosition.transform.rotation);
        b.GetComponent<NetworkObject>().Spawn(true);

        var bf = b.GetComponent<BombForce>();
        bf.forceStrength = power;
        bf.ApplyForce();
    }

    private void Throwtimer()
    {
        timer = ogTimer;
        timerThrow = false;
    }

    private void Timer()
    {
        if (timerThrow == false)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0) { timer = ogTimer; timerThrow = true; }
    }
}