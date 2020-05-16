using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    public float velocity;
    private Transform tr;
    private RatBehavior rat;

    void Start()
    {
        tr = GetComponent<Transform>();
        rat = GameObject.Find("Rat(Clone)").GetComponent<RatBehavior>();
    }

    private void FixedUpdate()
    {
        if(tr.position.x < -30)
        {
            Destroy(gameObject);
        }
        tr.position = new Vector2(tr.position.x - velocity * (0.2f + rat.speed/2), tr.position.y);

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
}
