using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum PlayerType
{
    Normal = 0,
    Infected = 1,
    Dead = 2,
}
public class PlayerCounterController : MonoBehaviour
{
    public TextMeshProUGUI PlayerNormalCountText;
    public TextMeshProUGUI PlayerInfectedCountText;
    public TextMeshProUGUI PlayerDeadCountText;

    public int MaxPlayersCount;
    public bool InLobby = false;
    public bool InMatchMaking = false;
    public int PlayersNormalCount { get; private set; }
    public int PlayersDeadCount { get; set; }
    public int PlayersInfectedCount { get; set; }

    public void Start()
    {
        PlayersNormalCount = GameObject.FindGameObjectsWithTag("Player").Length;
        if (InLobby)
            MaxPlayersCount = 200;
        else
            MaxPlayersCount = 30;
        UpdateText();
    }
    public void IncreasePlayers(PlayerType playerType)
    {
        Debug.Log("INC:" + playerType);
        switch (playerType)
        {
            case PlayerType.Normal:
                {
                    PlayersNormalCount++;
                }
                break;
            case PlayerType.Infected:
                {
                    PlayersInfectedCount++;
                }
                break;
            case PlayerType.Dead:
                {
                    PlayersDeadCount++;
                }
                break;
        }
        UpdateText();
        CheckForPlayersCount();
    }

    public void DecreasePlayers(PlayerType playerType)
    {
        Debug.Log("DEC:" + playerType);
        switch (playerType)
        {
            case PlayerType.Normal:
                {
                    PlayersNormalCount--;
                }
                break;
            case PlayerType.Infected:
                {
                    PlayersInfectedCount--;
                }
                break;
            case PlayerType.Dead:
                {
                    PlayersDeadCount--;
                }
                break;
        }
        UpdateText();
        CheckForPlayersCount();
    }

    private void UpdateText()
    {
        PlayerNormalCountText.SetText(PlayersNormalCount.ToString());
        PlayerInfectedCountText.SetText(PlayersInfectedCount.ToString());
        PlayerDeadCountText.SetText(PlayersDeadCount.ToString());
    }
    private void CheckForPlayersCount()
    {
        if (PlayersNormalCount == 0 && !InLobby)
        {
            if (InMatchMaking)
            {
                var time = FindObjectOfType<TimerController>().GetLastTime();
                FindObjectOfType<Leaderboard>().AddNewHighScore(FindObjectOfType<PlayerInfo>().PlayerName, time);
                PlayerPrefs.SetInt("RecordTime", (int)(time - TimerController.Offset));
                Debug.Log(time);
            }
            Instantiate(ObjectsContainer.instance.GameOverMenu);
        }
    }
}
