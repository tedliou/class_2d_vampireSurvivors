using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = .6f;
    public int ReboundCount = 1;
    public int Passthrough = 1;
    public float Distance = 0;
    public int Damage = 0;

    private Rigidbody2D _rb;
    private Vector3 _localDirection;
    private string _blockTag = "Block";
    private string _enemyTag = "Enemy";
    private int _currentReboundCount;
    private int _currentPassthroughCount;
    private float _currentDistance;
    private Vector3 _lastPos;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _localDirection = transform.right;
        _lastPos = transform.position;
        if (Distance == 0) Distance = 24;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_blockTag))
        {
            if (_currentReboundCount < ReboundCount)
            {
                _currentReboundCount++;

                var hit = Physics2D.Raycast(transform.position, transform.right);
                _localDirection = Vector2.Reflect(transform.right, hit.normal);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag(_enemyTag))
        {
            if (_currentPassthroughCount >= Passthrough)
            {
                Destroy(gameObject);
            }
            else
            {
                _currentPassthroughCount++;
            }
        }
    }
}
