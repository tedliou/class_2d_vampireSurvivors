using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSword : Projectile
{
    public float rotateSpeed = 10;
    public float distanceLimit = 10;
    public float thorwForce = 5;

    private Vector3 _originPos;
    private Rigidbody2D _rb;

    private void Start()
    {
        _originPos = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(transform.right * thorwForce);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 5 * Time.deltaTime * rotateSpeed));

        if (Vector3.Distance(transform.position, _originPos) > distanceLimit)
        {
            Destroy(gameObject);
        }
    }
}
