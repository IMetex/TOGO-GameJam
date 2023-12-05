using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Scirpts.Money;
using UnityEngine;

public class cAM : MonoBehaviour
{
    public CinemachineVirtualCamera _baraka;
    private float timer = 15f; // One minute in seconds
    private bool timerStarted = false;

    private void Update()
    {
        if (!timerStarted && BanknoteManager.Instance.GetBanknoteCount() > 5)
        {
            // Start the timer when the condition is met for the first time
            StartCoroutine(StartTimer());
            timerStarted = true;
        }
    }
    
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timer);
        // After one minute, set the priority to -4
        _baraka.Priority = -4;
    }
}
