using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    public int MapIndex = 0;

    public void ChooseMap()
    {
        SceneManager.LoadScene(MapIndex);
    }
}
