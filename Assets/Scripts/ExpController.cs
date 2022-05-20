using UnityEngine;

public class ExpController : MonoBehaviour
{
    public int exp = 1;

    private const float _moveSpeed = 1f;
    private ExpCollector _expCollector;

    private void Start()
    {
        //if (!_expCollector) _expCollector = FindObjectOfType<ExpCollector>();
    }

    private void Update()
    {
        if (!_expCollector) return;
        for (int i = 0; i < _moveSpeed; i++)
        {
            transform.Translate((_expCollector.transform.position - transform.position).normalized * Time.deltaTime);
            if (Vector3.Distance(_expCollector.transform.position, transform.position) < .05f)
            {
                GamePlayer.instance.GetExp(exp);
                Destroy(gameObject);
                return;
            }
        }
    }

    public void SetCollector(ExpCollector collector)
    {
        _expCollector = collector;
    }
}
