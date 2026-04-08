using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    private float actionActive = 0.3f;
    public GameObject fadeAction;
    private bool actionQuestion = false;

    private Rigidbody rb;

    public void Start()
    {
        fadeAction.SetActive(false);

        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (actionQuestion)
        {
            actionActive -= Time.deltaTime;
        }

        if (actionActive <= 0)
        {
            actionActive = 30f;
            fadeAction.SetActive(false);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            fadeAction.SetActive(true);
            actionQuestion = true;
        }
    }
}
