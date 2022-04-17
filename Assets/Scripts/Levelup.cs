using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Levelup : MonoBehaviour
{
    public static int guardianLevel = 1;
    public static int bowLevel = 0;
    public static int poisonLevel = 0;
    public static int swordLevel = 0;

    public Button guardian;
    public Button bow;
    public Button poison;
    public Button sword;

    private string guardianString;
    private string bowString;
    private string poisonString;
    private string swordString;

    private void Awake()
    {
        guardian.onClick.AddListener(() => Upgrade(1));
        bow.onClick.AddListener(() => Upgrade(2));
        poison.onClick.AddListener(() => Upgrade(3));
        sword.onClick.AddListener(() => Upgrade(4));

        guardianString = guardian.GetComponentInChildren<TMP_Text>().text;
        bowString = bow.GetComponentInChildren<TMP_Text>().text;
        poisonString = poison.GetComponentInChildren<TMP_Text>().text;
        swordString = sword.GetComponentInChildren<TMP_Text>().text;
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        guardian.GetComponentInChildren<TMP_Text>().text = string.Format(guardianString, guardianLevel);
        bow.GetComponentInChildren<TMP_Text>().text = string.Format(bowString, bowLevel);
        poison.GetComponentInChildren<TMP_Text>().text = string.Format(poisonString, poisonLevel);
        sword.GetComponentInChildren<TMP_Text>().text = string.Format(swordString, swordLevel);
    }

    public void Upgrade(int type)
    {
        gameObject.SetActive(false);
        switch (type)
        {
            case 1:
                guardianLevel += 1;
                break;
            case 2:
                bowLevel += 1;
                break;
            case 3:
                poisonLevel += 1;
                break;
            case 4:
                swordLevel += 1;
                break;
        }
    }
}
