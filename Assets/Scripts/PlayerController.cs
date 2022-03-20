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
    public List<Skill> BaseSkills;

    private PlayerAction _playerAction;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _playerAction = new PlayerAction();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        var skills = SkillParant.GetComponentsInChildren<Skill>();
        BaseSkills = new List<Skill>(skills);
    }

    private void OnEnable()
    {
        _playerAction.Enable();
    }

    private void OnDisable()
    {
        _playerAction.Disable();
    }

    private void Update()
    {
        var direction = _playerAction.Player_Map.Movememet.ReadValue<Vector2>();
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
        Movement = _playerAction.Player_Map.Movememet.ReadValue<Vector2>();

        _rigidBody.velocity = Movement * MoveSpeed;

        if (Movement != Vector2.zero)
        {
            if (Movement.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (Movement.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            
            LastMovement = Movement;
            LastPosition = transform.position;
        }
    }
}
