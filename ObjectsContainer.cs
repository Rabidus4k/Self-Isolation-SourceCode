using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsContainer : MonoBehaviour
{
    public static ObjectsContainer instance;
    public GameObject PlayerPrefab;
    public GameObject DeadPlayerPrefab;

    public GameObject GameOverMenu;

    public string PlayerName;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
