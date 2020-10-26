using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{
    public GameObject PlanePrefab;
    public Transform PlaneSpawnPoint;
    public Reload Reloader;
    private bool _canUse = true;
    private PlayerCounterController _playerCounterController;
    public bool InLobby = false;
    private void Start()
    {
        _playerCounterController = FindObjectOfType<PlayerCounterController>();
    }

    public void GetImunitet()
    {
        if (_canUse)
        {
            
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in players)
            {
                player.GetComponent<PlayerInteractions>().Imunitet();
            }
            if (!InLobby)
            {
                _canUse = false;
                Reloader.StartReload(() => { _canUse = true; }, 10f);
            }
                
        }
    }

    public void Thanos()
    {
        if (_canUse)
        {
            
            var players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length / 2; i++)
            {
                PlayerType thisPlayerType = players[i].GetComponent<PlayerInteractions>().ThisPlayerType;
                _playerCounterController.DecreasePlayers(thisPlayerType);
                Destroy(players[i]);
            }
            if (!InLobby)
            {
                _canUse = false;
                Reloader.StartReload(() => { _canUse = true; }, 5f);
            }
                
        }
    }

    public void SupportPlane()
    {
        if (_canUse)
        {
            
            Instantiate(PlanePrefab, PlaneSpawnPoint.position, Quaternion.identity);
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in players)
            {
                player.GetComponent<PlayerInteractions>().Heal();
            }
            if (!InLobby)
            {
                _canUse = false;
                Reloader.StartReload(() => { _canUse = true; }, 50f);
            }
                
        }
    }
}
