using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    public float velocity, baseVelocity, sinkSpeed;

    private Transform tr;
    private RatBehavior rat;

    private bool sink;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if(tr.position.x < -40 || tr.position.y < -15)
        {
            Destroy(gameObject);
        }
        float y = tr.position.y;
        if (sink)
        {
            y -= sinkSpeed;
        }
        if(rat == null)
        {
            rat = GameObject.Find("Rat(Clone)").GetComponent<RatBehavior>();
        }
        tr.position = new Vector2(tr.position.x - velocity * (baseVelocity + rat.speed / 2), y);

        if(rat == null)
        {
            rat = GameObject.Find("Rat(Clone)").GetComponent<RatBehavior>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox") && rat != null)
        {
            rat.GetHit();
        }
    }

    public void OnDeath()
    {
        sink = true;
    }
}
