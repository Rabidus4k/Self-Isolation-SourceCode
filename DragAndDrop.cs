using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragAndDrop : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private bool _isDragging = false;
    private PlayerCounterController _counterController;
    private PlayerInteractions _playerInteractions;
    
    private void Start()
    {
        
        _counterController = FindObjectOfType<PlayerCounterController>();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerInteractions = GetComponent<PlayerInteractions>();
    }
     private void Update()
    {
        if (_isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    #region MOUSE_EVENTS
    private void OnMouseUp()
    {
        _isDragging = false;
        _playerInteractions.CanMakeSons = true;
        _playerMovement.CanWalk = true;
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        _playerInteractions.CanMakeSons = false;
        _playerMovement.CanWalk = false;
    }
    #endregion
}
