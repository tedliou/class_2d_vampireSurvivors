using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : Skill
{
    public int Level = 0;
    public float Speed = 1;
    public float Radius = 3;
    public int Resulotion = 180;
    public GameObject Dot;
    public Stone Stone;

    private List<GameObject> _stones = new List<GameObject>();

    private void Update()
    {
        Level = Levelup.guardianLevel;
    }

    private void FixedUpdate()
    {
        Level = Mathf.Max(0, Level);
        if (_stones.Count < Level)
        {
            if (_stones.Count == 0 || (Vector3.Distance(new Vector3(0, Radius), _stones[_stones.Count - 1].transform.position) > 1f))
            {
                Stone.Damage = 100;
                Stone.transform.position = new Vector3(0, Radius);
                _stones.Add(Instantiate(Stone, transform).gameObject);
            }
        }

        //while (_stones.Count > Level)
        //{
        //    Destroy(_stones[_stones.Count - 1]);
        //    _stones.RemoveAt(_stones.Count - 1);
        //}

        for (int i = 0; i < _stones.Count; i++)
        {
            _stones[i].transform.RotateAround(transform.position, Vector3.forward, 360 * Time.fixedDeltaTime * Speed);
        }
    }
}
