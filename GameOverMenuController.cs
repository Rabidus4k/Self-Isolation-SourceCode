using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene(0);
    }
}
