using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundItem : MonoBehaviour
{
    public float speedMultiplier;

    private Transform tr;
    private RatBehavior rat;
    
    void Start()
    {
        rat = GameObject.Find("Rat(Clone)").GetComponent<RatBehavior>();
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (tr.position.x < -40)
        {
            Destroy(gameObject);
        }
        if (rat == null)
        {
            rat = GameObject.Find("Rat(Clone)").GetComponent<RatBehavior>();
        }
        tr.position = new Vector2(tr.position.x - rat.speed * speedMultiplier, tr.position.y);
    }
}
