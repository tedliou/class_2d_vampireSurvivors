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
    Hunter = 0,
    RebelHunter = 1
}

public class GamePlayer : MonoBehaviour
{
    public static GamePlayer instance;
    public static Canvas mainCanvas;

    [Header("Status")]
    public GameRole role;
    public int level;
    public int exp;
    public int health;
    public Vector2 direction;
    public List<ItemData> items;

    [Header("Settings")]
    public float moveSpeed;
    public float stopDistance;

    [Header("Components")]
    public new Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Canvas canvas;
    public DamageBubble damageBubble;
    public Transform damageBubbleTransform;
    public ExpController expPrefab;

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

    private void Start()
    {
        if (mainCanvas == null)
        {
            mainCanvas = canvas;
        }

        if (role == GameRole.Hunter)
        {
            instance = this;
        }

        level = 1;
        exp = 0;
        health = 100;
        items = new List<ItemData>();
    }

    private void FixedUpdate()
    {
        if (role != GameRole.Hunter)
        {
            if (Vector2.Distance(instance.transform.position,transform.position) < stopDistance)
            {
                SetMove(Vector2.zero);
            }
            else
            {
                SetMove((instance.transform.position - transform.position).normalized);
            }
        }
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (role == GameRole.Hunter)
        {
            if (collision.CompareTag("Enemy"))
            {
                GetDamage(5);
            }
        }
        else
        {
            if (collision.CompareTag("Bullet"))
            {
                GetDamage(20);
            }
        }
    }
    #endregion

    public void SetMove(InputAction.CallbackContext context)
    {
        SetMove(context.ReadValue<Vector2>());
    }

    public void SetMove(Vector2 direction)
    {
        this.direction = direction;
        ChangeAnimationState();
    }

    public void ChangeAnimationState()
    {
        var nextState = animator.GetFloat(_animationMode);
        if (direction.x < 0) nextState = 3;
        else if(direction.x > 0) nextState = 2;
        else if (direction.y < 0) nextState = 0;
        else if (direction.y > 0) nextState = 1;
        animator.SetFloat(_animationMode, nextState);
    }

    private void Move()
    {
        rigidbody.velocity = direction * moveSpeed;
    }

    [Button]
    public void GetDamage(int damage = 0)
    {
        var bubble = Instantiate(damageBubble, mainCanvas.transform);
        var pos = Camera.main.WorldToScreenPoint(damageBubbleTransform.position);
        pos.z = 0;
        bubble.rectTransform.anchoredPosition = pos;
        bubble.bubbleText.text = damage.ToString();
        health -= damage;
        if (health <= 0)
        {
            if (role == GameRole.Hunter)
            {
                // Game over
            }
            else
            {
                // Kill
                expPrefab.transform.position = transform.position;
                Instantiate(expPrefab);
                Destroy(gameObject);
            }
        }
    }

    public void GetExp(int exp)
    {
        this.exp += exp;
        if (this.exp >= 100)
        {
            this.exp = 0;
            level += 1;
            UIController.instance.CreateUpgradeOption();
            GameManager.Instance.PauseGame();
        }
    }
}
