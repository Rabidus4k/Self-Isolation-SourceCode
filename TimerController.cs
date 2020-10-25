using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public int Seconds = 0;
    public TextMeshProUGUI TimeText;

    private int _curMin;
    private int _curSec;

    private Coroutine _lastCoroutine;
    
    private void Start()
    {
        _lastCoroutine = StartCoroutine(Timer());    
    }

    private IEnumerator Timer()
    {
        _curSec = Seconds;
        while (true)
        {
            _curMin = _curSec / 60;
            var a = _curMin.ToString("D2");
            var b = (_curSec - _curMin * 60).ToString("D2");
            TimeText.SetText($"{a}:{b}");
            _curSec++;
            yield return new WaitForSeconds(1);
        }
    }

    public int GetLastTime()
    {
        StopCoroutine(_lastCoroutine);
        return _curSec;
    }
}
