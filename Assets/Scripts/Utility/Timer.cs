﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float m_timeLimit = 120f;

    public Text m_TimerText;

    private float timer = 0f;

    public delegate void EndTimerDelegate();
    public static event EndTimerDelegate m_EventEndTimer;

    private bool m_IsTiming = false;

    public void StartTimer()
    {
        timer = m_timeLimit;
        m_IsTiming = true;
    }

    public void ResumeTimer()
    {
        m_IsTiming = true;
    }

    public void PauseTimer()
    {
        m_IsTiming = false;
    }

    public void StartTimer(float newTime)
    {
        timer = Mathf.Clamp(newTime, 0, newTime);
    }

    void FixedUpdate()
    {
        if (m_IsTiming)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                //Evento de parada da fase
                if (m_EventEndTimer != null)
                    m_EventEndTimer();

                m_IsTiming = false;
            }
            else
            {
                float minutes = Mathf.Floor(timer / 60);

                float seconds = Mathf.Floor(timer % 60);

                string min = "";
                string sec = "";

                if (minutes < 10)
                    min = "0" + minutes.ToString("f0");
                else
                    min = minutes.ToString("f0");

                if (seconds < 10)
                    sec = "0" + seconds.ToString("f0");
                else
                    sec = seconds.ToString("f0");

                m_TimerText.text = min + ":" + sec;
            }
        }
    }
}
