using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bottle : Projectile
{
    public float Radius = 6;
    public float Interval = .1f;
    public float Duration = 3;
    public Transform Area;

    private float _duration;
    private Dictionary<EnemyController, float> _enemies = new Dictionary<EnemyController, float>();

    private void Update()
    {
        Area.localScale = Vector3.one * Radius;
        var enemies = Physics2D.OverlapCircleAll(transform.position, Radius * 0.5f, 1 << 7).Select(x => x.GetComponent<EnemyController>()).ToList();
        var except = _enemies.Keys.Except(enemies);
        foreach(var e in except)
        {
            _enemies.Remove(e);
        }

        foreach(var e in enemies)
        {
            var buff = e.Buff.Where(x => x.Type == BuffType.BottleDamage).FirstOrDefault();
            if (buff != null)
            {
                buff.CurrentDuration = 0;
            }
            else
            {
                e.Buff.Add(new Buff
                {
                    Type = BuffType.BottleDamage,
                    Damage = Damage,
                    Interval = Interval,
                    Duration = Duration
                });
            }
        }

        _duration += Time.deltaTime;
        if (_duration > Duration)
        {
            Destroy(gameObject);
        }
    }
}
