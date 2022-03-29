using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Component")]
    public TMP_Text level;
    public TMP_Text timer;
    public Slider expBar;

    [Header("String Format")]
    public string levelFormat = "Lv.{0}";
    public string timerFormat = "{0}:{1}";

    private void Start()
    {
        GameManager.OnTimerUpdate.AddListener(SetTimer);
        GameManager.OnLevelUpdate.AddListener(SetLevel);
        GameManager.OnExpUpdate.AddListener(SetExp);

        expBar.minValue = 0;
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
}
