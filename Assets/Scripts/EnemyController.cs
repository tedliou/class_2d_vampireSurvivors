using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int HP = 1000;
    public float Speed = .01f;

    private PlayerController _player;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _sr.material = Instantiate(_sr.material);
    }

    private void Update()
    {
        var current = _sr.material.GetFloat("_HitEffectBlend");
        current -= .05f;
        current = Mathf.Max(current, 0);
        _sr.material.SetFloat("_HitEffectBlend", current);
    }

    private void FixedUpdate()
    {
        var direction = (_player.transform.position - transform.position).normalized;
        _rb.velocity = direction * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            var damage = collision.gameObject.GetComponent<Bullet>().Damage;
            GetDamage(damage);
        }
    }

    private void GetDamage(int damage)
    {
        _sr.material.SetFloat("_HitEffectBlend", 1);
        HP -= damage;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
