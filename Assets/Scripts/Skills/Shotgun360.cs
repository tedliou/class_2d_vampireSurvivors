using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun360 : BaseSkill
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
        0.1f
    };

    private int[][] _spreads = new int[][]
    {
        new int[]{ 0, 3, -3 },
        new int[]{ 0, 3, -3, 7, -7 },
        new int[]{ 0, 3, -3, 7, -7, 15, -15 },
        new int[]{ 0, 3, -3, 7, -7, 15, -15, 30, -30 }
    };

    private IEnumerator Start()
    {
        while (true)
        {
            var level = Level - 1;
            transform.Rotate(Quaternion.Euler(0, 0, 45).eulerAngles);

            foreach (var e in _spreads[level])
            {
                var bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
                bullet.transform.right = transform.right;
                bullet.transform.rotation = Quaternion.Euler(0, 0, e) * bullet.transform.rotation;
                var bulletScr = bullet.GetComponent<Bullet>();
                bulletScr.Damage = _damage[level];
                bulletScr.Speed = _speeds[level];
                bulletScr.Distance = 0;
                bulletScr.Passthrough = 10;
            }

            yield return new WaitForSeconds(_cooldowns[level]);
        }
    }
}
