using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun360 : Skill
{
    [Range(1, 4)] public int Level = 1;

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
        0.3f,
        0.05f
    };

    private int[][] _spreads = new int[][]
    {
        new int[]{ 0, 3, -3 },
        new int[]{ 0, 3, -3, 7, -7 },
        new int[]{ 0, 3, -3, 7, -7, 15, -15 },
        new int[]{ 0, 3, -3, 7, -7, 15, -15, 30, -30 }
    };

    private float _timeDelta;

    private void Awake()
    {
        ObjectPool = new ObjectPool(Bullet, ObjectPoolParant);
    }

    private void FixedUpdate()
    {
        _timeDelta += Time.fixedDeltaTime;

        if (_timeDelta >= _cooldowns[Level - 1])
        {
            _timeDelta = 0;

            transform.Rotate(Quaternion.Euler(0, 0, 15).eulerAngles);

            var bullet = ObjectPool.Instantiate(transform.position);
            bullet.transform.right = transform.right;
            var bulletScr = bullet.GetComponent<Bullet>();
            bulletScr.Damage = _damage[Level - 1];
            bulletScr.Speed = _speeds[Level - 1];
            bulletScr.Distance = 24;
            bulletScr.Passthrough = 10;
            bulletScr.Parant = this;
        }
    }
}
