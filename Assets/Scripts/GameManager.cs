using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; set; }
    #endregion

    #region Event
    public static UnityEvent<int, int> OnTimerUpdate { get; set; } = new UnityEvent<int, int>();
    public static UnityEvent<int> OnLevelUpdate { get; set; } = new UnityEvent<int>();
    public static UnityEvent<int, int> OnExpUpdate { get; set; } = new UnityEvent<int, int>();
    #endregion

    #region Public
    [Header("Player")]
    public int level;
    public int exp;
    public int upgradeExpRequire;
    public int baseExpRequire = 5;
    public int increaseExpRequire = 2;

    [Header("Time")]
    public int Time;
    #endregion

    #region Private
    private Coroutine _timerTask;
    #endregion

    #region Message
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Invoke(nameof(StartGame), 1);
    }
    #endregion

    public void StartGame()
    {
        StartTimer();
        ResetPlayer();
    }


    #region Timer
    public void StartTimer()
    {
        ResetTimer();
        _timerTask = StartCoroutine(Timer());
    }

    private void ResetTimer()
    {
        Time = 0;
        OnTimerUpdate.Invoke(0, 0);
        if (_timerTask != null) StopCoroutine(_timerTask);
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            Time++;
            OnTimerUpdate.Invoke(Time / 60, Time % 60);
        }
    }
    #endregion

    #region Player
    private void ResetPlayer()
    {
        level = 0;
        exp = 0;
        UpdateUpgradeExpRequirement();
        OnLevelUpdate.Invoke(level);
        OnExpUpdate.Invoke(exp, upgradeExpRequire);
    }
    
    public void AddExp(int exp)
    {
        this.exp += exp;
        if (this.exp >= upgradeExpRequire)
        {
            this.exp = 0;
            level += 1;
            UpdateUpgradeExpRequirement();
            OnLevelUpdate.Invoke(level);
        }
        OnExpUpdate.Invoke(this.exp, upgradeExpRequire);
    }

    private void UpdateUpgradeExpRequirement()
    {
        upgradeExpRequire = 0;
        for (int i = 0; i < level + 1; i++)
        {
            upgradeExpRequire += baseExpRequire + increaseExpRequire * i;
        }
    }
    #endregion
}
