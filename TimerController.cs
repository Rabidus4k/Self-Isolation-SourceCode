﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static long Offset = long.MaxValue - 1000;
    public int Seconds = 0;
    public TextMeshProUGUI TimeText;

    private long _curMin;
    private long _curSec;

    private Coroutine _lastCoroutine;
    
    private void Start()
    {
        _lastCoroutine = StartCoroutine(Timer());    
    }

    private IEnumerator Timer()
    {
        _curSec = Seconds + Offset;
        while (true)
        {
            _curMin = (_curSec - Offset )/ 60;
            var a = _curMin.ToString("D2");
            var b = (_curSec - Offset - _curMin * 60).ToString("D2");
            TimeText.SetText($"{a}:{b}");
            _curSec++;
            yield return new WaitForSeconds(1);
        }
    }

    public long GetLastTime()
    {
        StopCoroutine(_lastCoroutine);
        return _curSec;
    }
}
