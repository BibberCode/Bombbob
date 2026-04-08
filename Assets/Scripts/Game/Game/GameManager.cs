using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float hearts = 10;

    void Start()
    {
        instance = this;
    }
}
