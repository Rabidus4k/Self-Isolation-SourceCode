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
        public int score;

        public Highscore(string _username, int _score)
        {
            username = _username;
            score = _score;
        }
    }

    const string _privateCode = "секретик";
    const string _publicCode = "секретик";
    const string _webURL = "секретик";


    public Highscore[] HighScoreList;
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
    public void AddNewHighScore(string name, int score)
    {
        StartCoroutine(UploadNewHighScore(name, score));
    }

    public void GetHighScoreList()
    {
        StartCoroutine(GetHighScores());
    }

    IEnumerator UploadNewHighScore(string username, int score)
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
        HighScoreList = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string tempUserName = entryInfo[0];
            int tempScore = int.Parse(entryInfo[1]);
            HighScoreList[i] = new Highscore(tempUserName, tempScore);
        }
    }

    private void AddInfoToTheList()
    {
        for (int i = 0; i < HighScoreList.Length; i++)
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
