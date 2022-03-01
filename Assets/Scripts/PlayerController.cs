using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    public Vector2 Movement;
    public PlayerAction PlayerAction;
    public Rigidbody2D Rigidbody2D;

    private void Awake()
    {
        PlayerAction = new PlayerAction();
        Rigidbody2D = GetComponent<Rigidbody2D>();
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
        }
    }
}
