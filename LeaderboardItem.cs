using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    public TextMeshProUGUI InfoText;
    public void SetInfoText(int index, string name, int score)
    {
        InfoText.SetText($"#{index + 1}\t{name.ToString().ToUpper()} ({score})");

        switch (index)
        {
            case 0:
                {
                    InfoText.color = Color.yellow;
                }
                break;
            case 1:
                {
                    InfoText.color = Color.yellow;
                }
                break;
            case 2:
                {
                    InfoText.color = Color.yellow;
                }
                break;
        }
    }
}
