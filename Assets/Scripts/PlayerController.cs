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
    public PlayerAction PlayerAction;
    public Rigidbody2D Rigidbody2D;

    [Header("Stats")]
    public bool Rush;
    public float CurrentRushDistance;
    public Vector2 LastMovement;
    public Vector2 LastPosition;
    public Vector2 MoveTargetPos;

    private void Awake()
    {
        PlayerAction = new PlayerAction();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerAction.Player_Map.Rush.started += _ =>
        {
            
        };
        PlayerAction.Player_Map.Rush.performed += _ =>
        {
            var hit = Physics2D.Raycast(transform.position, LastMovement, RushDistance, 1 << 0);
            if (hit)
            {
                MoveTargetPos = hit.point;
            }
            else
            {
                MoveTargetPos = (Vector2)transform.position + LastMovement.normalized * RushDistance;
            }
            Rush = true;
        };
        PlayerAction.Player_Map.Rush.canceled += _ =>
        {
            Rush = false;
        };
        CurrentRushDistance = RushDistance;
    }

    private void OnEnable()
    {
        PlayerAction.Enable();
    }

    private void OnDisable()
    {
        PlayerAction.Disable();
    }

    private void FixedUpdate()
    {
        Movement = PlayerAction.Player_Map.Movememet.ReadValue<Vector2>();

        if (Rush)
        {
            CurrentRushDistance = Vector2.Distance(transform.position, MoveTargetPos);
            Rigidbody2D.velocity = LastMovement * RushSpeed;
        }
        else
        {
            Rigidbody2D.velocity = Movement * MoveSpeed;

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
}
