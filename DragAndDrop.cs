using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragAndDrop : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    public bool IsDragging = false;
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
        if (IsDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    #region MOUSE_EVENTS
    private void OnMouseUp()
    {
        IsDragging = false;
        _playerInteractions.CanMakeSons = true;
        _playerMovement.CanWalk = true;
    }

    private void OnMouseDown()
    {
        IsDragging = true;
        _playerInteractions.CanMakeSons = false;
        _playerMovement.CanWalk = false;
    }
    #endregion
}
