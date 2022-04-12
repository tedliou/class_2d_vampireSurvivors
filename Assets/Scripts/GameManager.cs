using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; set; }
    #endregion

    #region Event
    public static UnityEvent OnDeath { get; set; } = new UnityEvent();
    public static UnityEvent<int, int> OnTimerUpdate { get; set; } = new UnityEvent<int, int>();
    public static UnityEvent<int> OnLevelUpdate { get; set; } = new UnityEvent<int>();
    public static UnityEvent<int> OnHPUpdate { get; set; } = new UnityEvent<int>();
    public static UnityEvent<int, int> OnExpUpdate { get; set; } = new UnityEvent<int, int>();
    #endregion

    #region Public
    [Header("Player")]
    public int level;
    public int exp;
    public int upgradeExpRequire;
    public int baseExpRequire = 5;
    public int increaseExpRequire = 2;
    public int hp;

    [Header("Time")]
    public int time;
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
        Time.timeScale = 1;
        StartTimer();
        ResetPlayer();
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartGame();
    }

    #region Timer
    public void StartTimer()
    {
        ResetTimer();
        _timerTask = StartCoroutine(Timer());
    }

    private void ResetTimer()
    {
        time = 0;
        OnTimerUpdate.Invoke(0, 0);
        if (_timerTask != null) StopCoroutine(_timerTask);
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            time++;
            OnTimerUpdate.Invoke(time / 60, time % 60);
        }
    }
    #endregion

    #region Player
    private void ResetPlayer()
    {
        level = 0;
        exp = 0;
        RefillHP();
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

    public void RefillHP()
    {
        hp = 100;
        OnHPUpdate.Invoke(hp);
    }

    public void ReduceHP(int point)
    {
        hp -= point;
        hp = Mathf.Max(0, hp);
        OnHPUpdate.Invoke(hp);
        if (hp == 0)
        {
            OnDeath.Invoke();
            StopGame();
        }
    }

    #endregion
}
