using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int VisibleLimit = 50;
    public GameObject Enemy;
    public float Interval = .05f;
    public float Radius = 12;

    private float _interval;

    private void Update()
    {
        if (EnemyController.VisibleEnemies.Count > VisibleLimit)
        {
            _interval = Interval;
            return;
        }

        _interval += Time.deltaTime;
        if (_interval > Interval)
        {
            _interval = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        var center = transform.position;
        var angle = Random.Range(0, 360);
        var x = center.x + Radius * Mathf.Cos(angle * Mathf.PI / 180);
        var y = center.y + Radius * Mathf.Sin(angle * Mathf.PI / 180);
        Enemy.transform.position = new Vector3(x, y, center.z);
        Instantiate(Enemy);
    }
}
