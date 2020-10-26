using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed = 1f;
    private Tilemap _tileMap;
    private Grid _grid;
    private Animator _animator;
    private Vector2 _moveDirection;
    private DragAndDrop _dnd;
    public bool CanWalk = true;

    private void Start()
    {
        _dnd = GetComponent<DragAndDrop>();
        _grid = FindObjectOfType<Grid>();
        _tileMap = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
        _animator = GetComponent<Animator>();
        ChangeDirection(false);
        StartCoroutine(RandomizeMovement());
    }

    // Update is called once per frame
    private void Update()
    {
        if (CanWalk)
        {
            transform.Translate(_moveDirection * Time.deltaTime * PlayerSpeed);
            _animator.SetBool("Walk", true);

            if (!_dnd.IsDragging)
            {
                Vector3Int posInt = _grid.WorldToCell(transform.position);
                posInt = new Vector3Int(posInt.x, posInt.y, posInt.z);
                if (null != _tileMap.GetTile(posInt))
                {
                    gameObject.GetComponent<PlayerInteractions>().Die();
                }
            }
        }
        else
        {
            _animator.SetBool("Walk", false);
        }
    }
    
    public void ChangeDirection(bool reverse)
    {
        if (!reverse)
            _moveDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        else
            _moveDirection = -_moveDirection;

        if (_moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private IEnumerator RandomizeMovement()
    {
        while (true)
        {
            var changeChance = Random.Range(0f, 1f);
            if (changeChance > 0.4f)
            {
                ChangeDirection(false);
            }
            var stopChance = Random.Range(0f, 1f);
            if (stopChance > 0.5f)
            {
                var randomTimeDelay = Random.Range(0f, 1f);
                CanWalk = false;
                yield return new WaitForSeconds(randomTimeDelay);
                CanWalk = true;
            }
            var randomTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(randomTime);
        }
    }
}
