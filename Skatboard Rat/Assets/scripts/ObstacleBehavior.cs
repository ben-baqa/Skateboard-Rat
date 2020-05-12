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
        rat = GameObject.Find("Rat").GetComponent<RatBehavior>();
    }

    private void FixedUpdate()
    {
        if(tr.position.x < -30)
        {
            Destroy(gameObject);
        }
        tr.position = new Vector2(tr.position.x - velocity * rat.speed, tr.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            rat.GetHit();
        }
    }
}
