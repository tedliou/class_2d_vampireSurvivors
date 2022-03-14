using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = .6f;
    public int ReboundCount = 0;
    public int Distance = 0;

    private Rigidbody2D _rb;
    private Vector3 _localDirection;
    private string _blockTag = "Block";
    private int _currentCollisionCount;
    private float _currentDistance;
    private Vector3 _lastPos;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _localDirection = transform.right;
        _lastPos = transform.position;
        if (Distance == 0) Distance = 100;
    }

    private void FixedUpdate()
    {
        _currentDistance += Vector3.Distance(transform.position, _lastPos);
        _lastPos = transform.position;
        if (_currentDistance > Distance)
        {
            Destroy(gameObject);
            return;
        }

        _rb.MovePosition(transform.position + _localDirection * Speed);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_blockTag))
        {
            if (_currentCollisionCount < ReboundCount)
            {
                _currentCollisionCount++;
                _localDirection = Vector2.Reflect(transform.right, collision.contacts[0].normal);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
