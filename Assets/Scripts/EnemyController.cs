using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    BottleDamage = 0
}

[System.Serializable]
public class Buff
{
    public BuffType Type;
    public int Damage;
    public float Interval;
    public float CurrentInterval;
    public float Duration;
    public float CurrentDuration;
    public bool MarkRemove;
}

public class EnemyController : MonoBehaviour
{
    public static List<EnemyController> VisibleEnemies = new List<EnemyController>();

    public ExpController exp;

    public int HP = 1000;
    public float Speed = .01f;
    public int damage = 10;

    public List<Buff> Buff = new List<Buff>();

    private PlayerController _player;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _sr.material = Instantiate(_sr.material);

        HP += GameManager.Instance.time;
    }

    private void Update()
    {
        var current = _sr.material.GetFloat("_HitEffectBlend");
        current -= .05f;
        current = Mathf.Max(current, 0);
        _sr.material.SetFloat("_HitEffectBlend", current);

        for (int i = 0; i < Buff.Count; i++)
        {
            Buff[i].CurrentInterval += Time.deltaTime;
            Buff[i].CurrentDuration += Time.deltaTime;
            if (Buff[i].CurrentInterval >= Buff[i].Interval)
            {
                Buff[i].CurrentInterval = 0;
                GetDamage(Mathf.Max(Buff[i].Damage, 0));
            }
            if (Buff[i].CurrentDuration >= Buff[i].Duration)
            {
                Buff[i].MarkRemove = true;
            }
        }
        Buff.RemoveAll(x => x.MarkRemove);

        
    }

    private void FixedUpdate()
    {
        var direction = (_player.transform.position - transform.position).normalized;
        _rb.velocity = direction * Speed;
        _sr.flipX = _rb.velocity.x < 0;
    }

    public void Knockback()
    {
        var direction = (transform.position - _player.transform.position).normalized;
        _rb.MovePosition(transform.position + direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            var damage = collision.gameObject.GetComponent<Projectile>().Damage;
            GetDamage(damage);
        }
    }

    private void GetDamage(int damage)
    {
        _sr.material.SetFloat("_HitEffectBlend", 1);
        HP -= damage;

        if (HP <= 0)
        {
            exp.transform.position = transform.position;
            Instantiate(exp);
            Destroy(gameObject);
        }
    }

    private void OnBecameVisible()
    {
        VisibleEnemies.Add(this);
    }

    private void OnBecameInvisible()
    {
        VisibleEnemies.Remove(this);
    }
}
