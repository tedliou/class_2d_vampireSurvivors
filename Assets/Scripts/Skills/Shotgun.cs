using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Skill
{
    [Range(0, 4)] public int Level = 0;

    public GameObject Bullet;

    private int[] _damage = new int[]
    {
        1,
        2,
        3,
        4
    };

    private float[] _speeds = new float[]
    {
        0.4f,
        0.5f,
        0.6f,
        0.7f
    };

    private float[] _cooldowns = new float[]
    {
        1.2f,
        0.8f,
        0.7f,
        0.6f
    };

    private int[][] _spreads = new int[][]
    {
        new int[]{ 0, 3, -3 },
        new int[]{ 0, 3, -3, 7, -7 },
        new int[]{ 0, 3, -3, 7, -7, 15, -15 },
        new int[]{ 0, 3, -3, 7, -7, 15, -15, 30, -30 }
    };

    private float _cooldown;

    private void Awake()
    {
        ObjectPool = new ObjectPool(Bullet, ObjectPoolParant);
    }

    private void Update()
    {
        Level = Levelup.bowLevel;
        if (Level == 0) return;
        _cooldown += Time.deltaTime;
        if (_cooldown > _cooldowns[Mathf.Clamp(Level - 1, 0, _cooldowns.Length - 1)])
        {
            _cooldown = 0;
            Fire();
        }
    }

    private void Fire()
    {
        var level = Level - 1;
        var nearbies = Physics2D.OverlapCircleAll(transform.position, 12, 1 << 7);
        if (nearbies.Length > 0)
        {
            Collider2D nearby = null;
            var distance = 0f;
            foreach (var e in nearbies)
            {
                var currentDist = Vector2.Distance(transform.position, e.transform.position);
                if (nearby == null)
                {
                    nearby = e;
                    distance = currentDist;
                    continue;
                }

                if (currentDist < distance)
                {
                    nearby = e;
                }
            }
            transform.right = (nearby.transform.position - transform.position).normalized;
        }
        else
        {
            transform.right = Direction;
        }


        foreach (var e in _spreads[Mathf.Clamp(level, 0, _spreads.Length - 1)])
        {
            var bullet = ObjectPool.Instantiate(transform.position);
            bullet.transform.right = transform.right;
            bullet.transform.rotation = Quaternion.Euler(0, 0, e) * bullet.transform.rotation;
            var bulletScr = bullet.GetComponent<Bullet>();
            bulletScr.Damage = 100 + Level * 10;
            bulletScr.Speed = .4f + Level * .1f;
            bulletScr.Distance = 24;
            bulletScr.Passthrough = 10;
            bulletScr.Parant = this;
        }
    }
}
