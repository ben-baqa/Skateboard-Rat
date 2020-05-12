using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float jumpStrength;
    private RatBehavior rat;
    private Rigidbody2D rb;
    private Transform tr;

    void Start()
    {
        rat = GetComponentInParent<RatBehavior>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        tr.position = new Vector2(rat.transform.position.x, tr.position.y);
    }

    public void Jump()
    {
        rb.AddForce(jumpStrength * Vector2.up, ForceMode2D.Impulse);
    }
}
