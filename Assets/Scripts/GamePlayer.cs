using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using TMPro;
using Hunter2D;
using System.Linq;

namespace Hunter2D
{
    [System.Serializable]
    public class GameBuff
    {
        public float moveSpeed;
        public int attackDamage;
        public float attackSpeed;

        public GameBuff()
        {
            moveSpeed = 0;
            attackDamage = 0;
            attackSpeed = 0;
        }

        public static GameBuff operator +(GameBuff aBuff, GameBuff bBuff)
        {
            aBuff.moveSpeed += bBuff.moveSpeed;
            aBuff.attackDamage += bBuff.attackDamage;
            aBuff.attackSpeed += bBuff.attackSpeed;
            return aBuff;
        }
    }
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
    public GameBuff buff;

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
    public Animator[] baseGuns;

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
        mainCanvas = null;
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
        buff = new GameBuff();

        StartCoroutine(AttackLoop());
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
                //GetDamage(10);
            }
        }
        else
        {
            if (collision.CompareTag("Bullet"))
            {
                GetDamage(20 + instance.buff.attackDamage);
            }
        }
    }
    #endregion

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Mathf.Max(1 - buff.attackSpeed, 0));
            if (role == GameRole.Hunter)
            {
                foreach (var a in baseGuns)
                {
                    a.SetTrigger("Fire");
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, instance.transform.position) < .1f)
                {
                    instance.GetDamage(10);
                }
            }
        }
    }

    public void AddItem(ItemData item)
    {
        items.Add(item);
        UpdateBuffTable();
    }

    private void UpdateBuffTable()
    {
        buff = new GameBuff();
        foreach(var item in items)
        {
            buff += item.buff;
        }
    }

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
        rigidbody.velocity = direction * (moveSpeed + buff.moveSpeed);
    }

    [Button]
    public void GetDamage(int damage = 0)
    {
        var bubble = Instantiate(damageBubble, mainCanvas.transform);
        var pos = Camera.main.WorldToScreenPoint(damageBubbleTransform.position);
        pos.z = 0;
        bubble.transform.position = pos;
        bubble.bubbleText.text = damage.ToString();
        health -= damage;
        if (health <= 0)
        {
            if (role == GameRole.Hunter)
            {
                // Game over
                GameManager.Instance.StopGame();
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
        health = Mathf.Min(health + 20, 100);
        this.exp += exp;
        if (this.exp >= 100)
        {
            health = 100;
            this.exp = 0;
            level += 1;
            UIController.instance.CreateUpgradeOption();
            GameManager.Instance.PauseGame();
        }
    }

    public int GetItemCount(ItemData itemData)
    {
        return items.FindAll(x => x == itemData).Count;
    }
}
