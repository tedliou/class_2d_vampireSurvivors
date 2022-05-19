using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using TMPro;
using Hunter2D;

namespace Hunter2D
{

}
public enum GameRole
{
    Hunter = 0
}

public class GamePlayer : MonoBehaviour
{
    public static GamePlayer instance;

    [Header("Status")]
    public GameRole role;
    public int level;
    public int exp;
    public int health;
    public Vector2 direction;
    public List<ItemData> items;

    [Header("Settings")]
    public float moveSpeed;

    [Header("Components")]
    public new Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Canvas canvas;
    public DamageBubble damageBubble;
    public Transform damageBubbleTransform;

    private readonly string _animationMode = "Mode";

    #region Unity Message

#if UNITY_EDITOR
    private void OnValidate()
    {
        GetReqireComponent();
    }

    private void GetReqireComponent()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
#endif

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameManager.Instance.ReduceHP(collision.GetComponent<EnemyController>().damage);
        }
    }
    #endregion

    public void SetMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        Debug.Log($"Input direction: {direction}");
        ChangeAnimationState();
    }

    public void ChangeAnimationState()
    {
        var nextState = animator.GetFloat(_animationMode);
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
            nextState = 1;
        }
        else
        {
            if (direction.x > 0)
            {
                nextState = 1;
                spriteRenderer.flipX = false;
            }
            else if (direction.y < 0) nextState = 0;
            else if (direction.y > 0) nextState = 2;
        }
        animator.SetFloat(_animationMode, nextState);
    }

    private void Move()
    {
        rigidbody.velocity = direction * moveSpeed;
    }

    [Button]
    public void GetDamage(int damage = 0)
    {
        var bubble = Instantiate(damageBubble, canvas.transform);
        var pos = Camera.main.WorldToScreenPoint(damageBubbleTransform.position);
        pos.z = 0;
        bubble.rectTransform.anchoredPosition = pos;
        bubble.bubbleText.text = damage.ToString();
    }
}
