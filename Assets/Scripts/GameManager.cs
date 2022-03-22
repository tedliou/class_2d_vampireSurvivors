using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Instantiate(Resources.Load<GameManager>(nameof(GameManager)));
            }
            return _instance;
        }
    }
    private static GameManager _instance;
    #endregion

    #region Events
    public static UnityEvent<int> OnTimerUpdate = new UnityEvent<int>();
    #endregion

    #region Public
    public int Time;
    #endregion

    #region Private

    #endregion

    #region Message
    private void Start()
    {
        
    }
    #endregion

    public void StartTiming()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        Time = 0;
        OnTimerUpdate.Invoke(Time);
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            Time++;
            OnTimerUpdate.Invoke(Time);
        }
    }
}
