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
    public GameObject SandBoxMenu;
    public GameObject UpdateMessageMenu;
    public GameObject MapSelectionMenu;
    public TMP_InputField NameInputField;
    public void Start()
    {
        NameInputField.text = PlayerPrefs.GetString("SelfIsolationPlayerName");
        Time.timeScale = 1;
    }
    public void ShowLeaderBoard()
    {
        Instantiate(LeaderBoardMenu);
    }

    public void HideUpdateMessageMenu()
    {
        UpdateMessageMenu.SetActive(false);
    }

    public void GoMatchmaking()
    {
        PlayerPrefs.SetString("SelfIsolationPlayerName", NameInputField.text);
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
        MapSelectionMenu.SetActive(false);
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
        SandBoxMenu.SetActive(false);
        MainMenu.SetActive(false);
        GameChooseMenu.SetActive(false);
        MapSelectionMenu.SetActive(true);
    }
    
    public void OpenDownloadPage()
    {
        Application.OpenURL("https://www.yadi.sk/d/AG5ZSneGDItiNA");
    }
}
