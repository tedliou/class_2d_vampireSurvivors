using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Config")]
    public float MoveSpeed;
    public Vector2 Movement;
    public float RushSpeed;
    public float RushDistance;

    [Header("Stats")]
    public bool Rush;
    public float CurrentRushDistance;
    public Vector2 LastMovement;
    public Vector2 LastPosition;
    public Vector2 MoveTargetPos;

    public Transform SkillParant;
    public List<BaseSkill> BaseSkills;

    private PlayerAction _pa;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _pa = new PlayerAction();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        var skills = SkillParant.GetComponentsInChildren<BaseSkill>();
        BaseSkills = new List<BaseSkill>(skills);
    }

    private void OnEnable()
    {
        _pa.Enable();
    }

    private void OnDisable()
    {
        _pa.Disable();
    }

    private void Update()
    {
        var direction = _pa.Player_Map.Movememet.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            foreach (var e in BaseSkills)
            {
                e.Direction = direction;
           }
        }
    }

    private void FixedUpdate()
    {
        Movement = _pa.Player_Map.Movememet.ReadValue<Vector2>();

        _rb.velocity = Movement * MoveSpeed;

        if (Movement != Vector2.zero)
        {
            var scale = transform.localScale;
            if (Movement.x < 0)
            {
                scale.x = -1;
            }
            else
            {
                scale.x = 1;
            }
            transform.localScale = scale;
            LastMovement = Movement;
            LastPosition = transform.position;
        }
    }
}
