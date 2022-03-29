using UnityEngine;

public class ExpCollector : MonoBehaviour
{
    public int collectorLevel = 0;
    public float baseRadius = 1.5f;

    private const string _expTag = "EXP";
    private CircleCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        _collider.radius = baseRadius + collectorLevel * .5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(_expTag)) return;
        collision.GetComponent<ExpController>()?.SetCollector(this);
    }
}
