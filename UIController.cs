using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject LeaderBoardMenu;
    public GameObject MainMenu;
    public GameObject GameChooseMenu;
    public TMP_InputField NameInputField;
    public GameObject SandBoxMenu;

    public void Start()
    {
        Time.timeScale = 1;
    }
    public void ShowLeaderBoard()
    {
        Instantiate(LeaderBoardMenu);
    }

    public void GoMatchmaking()
    {
        ObjectsContainer.instance.PlayerName = NameInputField.text;
        SceneManager.LoadScene(1);
    }
    public void Forward()
    {
        MainMenu.SetActive(false);
        GameChooseMenu.SetActive(true);
    }
    public void Back()
    {
        MainMenu.SetActive(true);
        GameChooseMenu.SetActive(false);    
    }
    public void QuitTheGame()
    {
        Application.Quit();
    }
    
    public void LeaveSandbox()
    {
        SceneManager.LoadScene(0);
    }

    public void StartSandbox()
    {
        SandBoxMenu.SetActive(true);
        MainMenu.SetActive(false);
        GameChooseMenu.SetActive(false);
    }
}
