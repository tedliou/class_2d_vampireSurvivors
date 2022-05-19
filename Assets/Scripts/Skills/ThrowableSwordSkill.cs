using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSwordSkill : Skill
{
    public float cooldown;
    public GameObject swordPrefab;

    private float _currentTime;

    private void Start()
    {
        _currentTime = 0;
    }

    private void Update()
    {
        if (Levelup.swordLevel == 0) return;
        _currentTime += Time.deltaTime;
        if (_currentTime > Mathf.Max(cooldown, cooldown - Levelup.swordLevel * .1f))
        {
            _currentTime = 0;
            swordPrefab.transform.position = transform.position;

            for (int i = 0; i < 360; i+=360 / Levelup.swordLevel)
            {
                var x = 10 * Mathf.Cos(i * Mathf.PI / 180);
                var y = 10 * Mathf.Sin(i * Mathf.PI / 180);
                swordPrefab.transform.right = new Vector2(x, y);
                Instantiate(swordPrefab);
            }
        }
    }
}
