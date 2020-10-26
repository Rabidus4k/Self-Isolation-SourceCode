using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;
using System;
using TMPro;

public class VersionCheck : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    public int CurrentGameVersion = 0;
    public int NewGameVersion = 0;
    public string UpdateMessage = string.Empty;

    public GameObject MessageMenu;
    public TextMeshProUGUI VersionText;

    private void Start()
    {
        VersionText.SetText($"v1.{CurrentGameVersion}");
        ConfigManager.FetchCompleted += GetData;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void GetData(ConfigResponse response)
    {
        NewGameVersion = ConfigManager.appConfig.GetInt("gameVersion");
        if (CurrentGameVersion != NewGameVersion)
        {
            UpdateMessage = ConfigManager.appConfig.GetString("updateMessage");
            ShowMessage();
        }
    }


    private void ShowMessage()
    {
        MessageMenu.SetActive(true);
        MessageMenu.GetComponent<Message>().SetText(UpdateMessage);
    }

    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= GetData;
    }
}
