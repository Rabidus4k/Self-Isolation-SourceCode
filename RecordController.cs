using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordController : MonoBehaviour
{
    public TextMeshProUGUI RecordTimeText;
    private void Start()
    {
        var curSec = PlayerPrefs.GetInt("RecordTime");
        var curMin = curSec / 60;
        var a = curMin.ToString("D2");
        var b = (curSec - curMin * 60).ToString("D2");
        RecordTimeText.SetText($"{a}:{b}");
    }

}
