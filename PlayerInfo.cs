using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public string PlayerName;
    public TextMeshProUGUI PlayerNameText;

    private void Start()
    {
        PlayerName = ObjectsContainer.instance.PlayerName;
        if (PlayerName == string.Empty)
            PlayerName = "Player_" + Random.Range(0, 1000);
        PlayerNameText.SetText(PlayerName);
    }
}
