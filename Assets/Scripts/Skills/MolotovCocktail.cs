using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovCocktail : Skill
{
    public float Radius = 6;
    public int Step = 6;
    public float Cooldown = .5f;
    public float DestroyTime = .5f;
    public float Duration = 3;
    public float Range = 3;
    public Bottle Bottle;

    private int _step;
    private float _cooldown;
    private int _index;
    private int _angle;

    private void Update()
    {
        Cooldown = 5f / Levelup.poisonLevel;
        _step = 360 / Step;
        _cooldown += Time.deltaTime;
        if (_cooldown > Cooldown)
        {
            _cooldown = 0;
            _index++;
            if (_index > Step)
            {
                _index = 1;
            }

            _angle = _step * _index;
            var x = transform.position.x + Radius * Mathf.Cos(_angle * Mathf.PI / 180);
            var y = transform.position.y + Radius * Mathf.Sin(_angle * Mathf.PI / 180);
            Bottle.transform.position = new Vector3(x, y, transform.position.z);
            Bottle.Duration = Duration;
            Bottle.Radius = Range;
            Instantiate(Bottle);
        }
    }
}
