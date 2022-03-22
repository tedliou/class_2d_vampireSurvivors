using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TMP_Text _text;

    #region Message
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        GameManager.OnTimerUpdate.AddListener(UpdateContent);
        GameManager.Instance.StartTiming();
    }

    private void OnDisable()
    {
        GameManager.OnTimerUpdate.RemoveListener(UpdateContent);
    }
    #endregion

    private void UpdateContent(int second)
    {
        var min = second / 60;
        var sec = second - min * 60;
        _text.text = $"{min}:{sec.ToString("00")}";
    }
}
