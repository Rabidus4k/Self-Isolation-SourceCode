using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{
    public Image ReloadImage;
    public float ReloadTime = 30;

    private Action toDoAction;

    public void StartReload(Action method, float time)
    {
        ReloadTime = time;
        gameObject.SetActive(true);
        toDoAction = method;
        StartCoroutine(ReloadCoroutine());
    }
    
    private IEnumerator ReloadCoroutine()
    {
        var secondsLeft = 0;
        ReloadImage.fillAmount = 1f;
        
        while (secondsLeft < ReloadTime)
        {
            ReloadImage.fillAmount = (1.0f / ReloadTime) * secondsLeft;
            secondsLeft++;
            yield return new WaitForSeconds(1);
        }

        ReloadImage.fillAmount = 0;
        toDoAction();
        gameObject.SetActive(false);
    }
}
