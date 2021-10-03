using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameSystem;

public class Timer : MonoBehaviour
{
    public TMP_Text timeText;

    void Awake()
    {
        TimerManager.ResetTimer();
        TimerManager.StartTimer();
    }

    void Update()
    {
        if (!TimerManager.IsTimerRunning())
        {
            return;
        }

        if (TimerManager.GetTimeRemaining() > 0)
        {
            TimerManager.SubtractTimeRemaining(Time.deltaTime);
            timeText.text = TimerManager.GetDisplayTimeRemaining();
        }
        else
        {
            TimerManager.StopTimer();
            Debug.Log("Time has run out!");
        }
    }
}
