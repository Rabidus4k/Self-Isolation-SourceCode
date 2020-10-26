using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public struct Highscore
    {
        public string username;
        public long score;

        public Highscore(string _username, long _score)
        {
            username = _username;
            score = _score;
        }
    }



    public List<Highscore> HighScoreList;
    public GameObject HighScoreListItemPrefab;
    public GameObject ListContainer;
    public TextMeshProUGUI ErrorText;
    public bool InLobby = true;

    private void Start()
    {
        if (InLobby)
        {
            transform.parent.gameObject.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
            GetHighScoreList();
        }
    }
    public void AddNewHighScore(string name, long score)
    {
        StartCoroutine(UploadNewHighScore(name, score));
    }

    public void GetHighScoreList()
    {
        StartCoroutine(GetHighScores());
    }

    IEnumerator UploadNewHighScore(string username, long score)
    {
        WWW www = new WWW(_webURL + _privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("successfully send");
        }
        else
        {
            Debug.Log("not successfully send " + www.error);
        }
    }

    IEnumerator GetHighScores()
    {
        WWW www = new WWW(_webURL + _publicCode + "/pipe/");
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighScores(www.text);
            AddInfoToTheList();
            ErrorText.SetText(string.Empty);
        }
        else
        {
            ErrorText.SetText(www.error);
        }
    }

    void FormatHighScores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        HighScoreList = new List<Highscore>();
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string tempUserName = entryInfo[0];
            long tempScore = long.Parse(entryInfo[1]);

            if (tempScore - TimerController.Offset > 0)
                HighScoreList.Add(new Highscore(tempUserName, tempScore - TimerController.Offset));
        }
    }

    private void AddInfoToTheList()
    {
        for (int i = 0; i < HighScoreList.Count; i++)
        {
            var tempItem = Instantiate(HighScoreListItemPrefab, ListContainer.transform);
            tempItem.GetComponent<LeaderboardItem>().SetInfoText(i, HighScoreList[i].username, HighScoreList[i].score);
        }
    }

    public void CloseLeaderBoard()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
