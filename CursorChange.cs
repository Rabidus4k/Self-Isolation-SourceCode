using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorChange : MonoBehaviour
{
    public Sprite NormalCursor;
    public Sprite DragCursor;

    public float OffsetY = 0;

    private SpriteRenderer _spriteRenderer;
    private Camera _camera;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
        Cursor.visible = false;
    }
    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = _camera.pixelHeight - currentEvent.mousePosition.y;

        point = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y + OffsetY, _camera.nearClipPlane));
        transform.position = new Vector3(point.x, point.y, 0);
#if UNITY_EDITOR

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + _camera.pixelWidth + ":" + _camera.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
#endif
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _spriteRenderer.sprite = DragCursor;
        } 
        else if (Input.GetMouseButtonUp(0))
        {
            _spriteRenderer.sprite = NormalCursor;
        }
    }
}
