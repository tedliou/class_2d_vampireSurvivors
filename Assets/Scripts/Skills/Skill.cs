using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public Vector2 Direction = Vector2.right;
    public ObjectPool ObjectPool;
    public Transform ObjectPoolParant;
}
