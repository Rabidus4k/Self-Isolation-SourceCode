using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBoxController : MonoBehaviour
{
    private AudioSource _audioSource;
    private PlayerCounterController _playerCounterController;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerCounterController = FindObjectOfType<PlayerCounterController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _audioSource.Play();
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 spawnPos = new Vector3(pos.x, pos.y, 0);
            _playerCounterController.IncreasePlayers(PlayerType.Normal);
            Instantiate(ObjectsContainer.instance.PlayerPrefab, spawnPos, Quaternion.identity);
        }
    }
}
