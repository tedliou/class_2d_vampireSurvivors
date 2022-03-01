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
    public bool ColliderTrigger;
    public bool Rush;
    public float CurrentRushDistance;
    public Vector2 LastMovement;
    public Vector2 LastPosition;

    private void Awake()
    {
        PlayerAction = new PlayerAction();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerAction.Player_Map.Rush.performed += _ =>
        {
            RaycastHit2D hit;
            if (Physics2D.Raycast(transform.position, LastMovement))
            {

            }

            if (CurrentRushDistance == RushDistance)
            {
                Debug.Log("Rush");
                Rush = true;
            }
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

        if (Rush && !ColliderTrigger)
        {
            Rigidbody2D.velocity = LastMovement * RushSpeed;
            CurrentRushDistance = CurrentRushDistance - Vector2.Distance(transform.position, LastPosition);
            if (CurrentRushDistance <= 0)
            {
                CurrentRushDistance = RushDistance;
                Rush = false;
            }
        }
        else
        {
            ColliderTrigger = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ColliderTrigger = true;
    }
}
