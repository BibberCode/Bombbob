using TouchControlsKit;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

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
        if (!IsOwner) return;

        Timer();

        // Power aufladen
        if (Mouse.current.leftButton.isPressed || TCKInput.GetAction("fireBtn", EActionEvent.Down))
        {
            currentPower += chargeRate * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, minPower, maxPower);
        }

        if ((Mouse.current.leftButton.wasReleasedThisFrame || TCKInput.GetAction("fireBtn", EActionEvent.Up)) && timer == ogTimer)
        {
            RequestThrowServerRpc(currentPower, throwAim.position, throwAim.rotation);
            Throwtimer();
            currentPower = minPower;
        }
    }

    // CLIENT → SERVER
    [ServerRpc]
    private void RequestThrowServerRpc(float power, Vector3 pos, Quaternion rot)
    {
        GameObject b = Instantiate(bomb, pos, rot);
        b.GetComponent<NetworkObject>().Spawn();

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