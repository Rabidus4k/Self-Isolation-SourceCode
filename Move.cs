using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 MoveDirection = Vector3.left;
    public float MoveSpeed = .5f;
    public float AliveTime = 10f;
    private void Start()
    {
        Destroy(gameObject, AliveTime);
    }
    private void Update()
    {
        transform.Translate(MoveDirection * Time.deltaTime * MoveSpeed);
    }
}
