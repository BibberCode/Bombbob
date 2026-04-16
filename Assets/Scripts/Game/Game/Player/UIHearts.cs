using UnityEngine;

public class UIHearts : MonoBehaviour
{
    [Header("Hearts")]
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public GameObject Heart4;
    public GameObject Heart5;
    public GameObject Heart6;
    public GameObject Heart7;
    public GameObject Heart8;
    public GameObject Heart9;
    public GameObject Heart10;

    [Header("Dead Hearts")]
    public GameObject deadHeart1;
    public GameObject deadHeart2;
    public GameObject deadHeart3;
    public GameObject deadHeart4;
    public GameObject deadHeart5;
    public GameObject deadHeart6;
    public GameObject deadHeart7;
    public GameObject deadHeart8;
    public GameObject deadHeart9;
    public GameObject deadHeart10;

    [Header("Half Hearts")]
    public GameObject halfHearts1;
    public GameObject halfHearts2;
    public GameObject halfHearts3;
    public GameObject halfHearts4;
    public GameObject halfHearts5;
    public GameObject halfHearts6;
    public GameObject halfHearts7;
    public GameObject halfHearts8;
    public GameObject halfHearts9;
    public GameObject halfHearts10;


    [SerializeField] private GameManager GameManager;

    private void Update()
    {
        Hearts();
        HalfHearts();
    }

    private void Hearts()
    {
        if (GameManager.hearts >= 1) { Heart1.SetActive(true); deadHeart1.SetActive(false); } else { Heart1.SetActive(false); deadHeart1.SetActive(true); }
        if (GameManager.hearts >= 2) { Heart2.SetActive(true); deadHeart2.SetActive(false); } else { Heart2.SetActive(false); deadHeart2.SetActive(true); }
        if (GameManager.hearts >= 3) { Heart3.SetActive(true); deadHeart3.SetActive(false); } else { Heart3.SetActive(false); deadHeart3.SetActive(true); }
        if (GameManager.hearts >= 4) { Heart4.SetActive(true); deadHeart4.SetActive(false); } else { Heart4.SetActive(false); deadHeart4.SetActive(true); }
        if (GameManager.hearts >= 5) { Heart5.SetActive(true); deadHeart5.SetActive(false); } else { Heart5.SetActive(false); deadHeart5.SetActive(true); }
        if (GameManager.hearts >= 6) { Heart6.SetActive(true); deadHeart6.SetActive(false); } else { Heart6.SetActive(false); deadHeart6.SetActive(true); }
        if (GameManager.hearts >= 7) { Heart7.SetActive(true); deadHeart7.SetActive(false); } else { Heart7.SetActive(false); deadHeart7.SetActive(true); }
        if (GameManager.hearts >= 8) { Heart8.SetActive(true); deadHeart8.SetActive(false); } else { Heart8.SetActive(false); deadHeart8.SetActive(true); }
        if (GameManager.hearts >= 9) { Heart9.SetActive(true); deadHeart9.SetActive(false); } else { Heart9.SetActive(false); deadHeart9.SetActive(true); }
        if (GameManager.hearts >= 10) { Heart10.SetActive(true); deadHeart10.SetActive(false); } else { Heart10.SetActive(false); deadHeart10.SetActive(true); }
    }

    private void HalfHearts()
    {
        if (GameManager.hearts == 0.5) { halfHearts1.SetActive(true); Heart1.SetActive(false); deadHeart1.SetActive(false); } else { halfHearts1.SetActive(false); }
        if (GameManager.hearts == 1.5) { halfHearts2.SetActive(true); Heart2.SetActive(false); deadHeart2.SetActive(false); } else { halfHearts2.SetActive(false); }
        if (GameManager.hearts == 2.5) { halfHearts3.SetActive(true); Heart3.SetActive(false); deadHeart3.SetActive(false); } else { halfHearts3.SetActive(false); }
        if (GameManager.hearts == 3.5) { halfHearts4.SetActive(true); Heart4.SetActive(false); deadHeart4.SetActive(false); } else { halfHearts4.SetActive(false); }
        if (GameManager.hearts == 4.5) { halfHearts5.SetActive(true); Heart5.SetActive(false); deadHeart5.SetActive(false); } else { halfHearts5.SetActive(false); }
        if (GameManager.hearts == 5.5) { halfHearts6.SetActive(true); Heart6.SetActive(false); deadHeart6.SetActive(false); } else { halfHearts6.SetActive(false); }
        if (GameManager.hearts == 6.5) { halfHearts7.SetActive(true); Heart7.SetActive(false); deadHeart7.SetActive(false); } else { halfHearts7.SetActive(false); }
        if (GameManager.hearts == 7.5) { halfHearts8.SetActive(true); Heart8.SetActive(false); deadHeart8.SetActive(false); } else { halfHearts8.SetActive(false); }
        if (GameManager.hearts == 8.5) { halfHearts9.SetActive(true); Heart9.SetActive(false); deadHeart9.SetActive(false); } else { halfHearts9.SetActive(false); }
        if (GameManager.hearts == 9.5) { halfHearts10.SetActive(true); Heart10.SetActive(false); deadHeart10.SetActive(false); } else { halfHearts10.SetActive(false); }
    }
}