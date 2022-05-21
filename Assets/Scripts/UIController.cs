using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Component")]
    public TMP_Text level;
    public TMP_Text timer;
    public Slider expBar;
    public TMP_Text message;
    public Slider hpBar;
    public GameObject deathScreen;
    public GameObject menu;
    public Hunter2D.UpgradeOptions upgradeOptions;
    public Transform overlayCanvas;

    [Header("String Format")]
    public string levelFormat = "Lv.{0}";
    public string timerFormat = "{0}:{1}";

    private Coroutine _hideMessageTask;

    private void Awake()
    {
        instance = this;
    }

    private async void Start()
    {
        GameManager.OnTimerUpdate.AddListener(SetTimer);
        //GameManager.OnLevelUpdate.AddListener(SetLevel);
        //GameManager.OnExpUpdate.AddListener(SetExp);
        //GameManager.OnHPUpdate.AddListener(SetHP);
        //GameManager.OnDeath.AddListener(() => deathScreen.SetActive(true));

        while (!GamePlayer.instance) await System.Threading.Tasks.Task.Yield();
        SetExp(GamePlayer.instance.exp, 100);
        SetLevel(1);
        SetHP(100);
    }

    private void Update()
    {
        SetExp(GamePlayer.instance.exp, 100);
        SetLevel(GamePlayer.instance.level);
        SetHP(GamePlayer.instance.health);
    }

    public void SetLevel(int level)
    {
        this.level.text = string.Format(levelFormat, level);
    }

    public void SetTimer(int minute, int second)
    {
        timer.text = string.Format(timerFormat, minute.ToString("00"), second.ToString("00"));
    }

    public void SetExp(int exp, int expMax)
    {
        expBar.maxValue = expMax;
        expBar.value = exp;
    }

    public void SetHP(int hp)
    {
        hpBar.value = hp;
    }

    public void CreateUpgradeOption()
    {
        Instantiate(upgradeOptions, overlayCanvas).transform.SetAsLastSibling();
    }
}
