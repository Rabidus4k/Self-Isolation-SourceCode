using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    public TextMeshProUGUI MessageText;
    
    public void SetText(string newMessage)
    {
        MessageText.SetText(newMessage);
    }
}
