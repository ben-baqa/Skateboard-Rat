using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatBehavior : MonoBehaviour
{
    public float pushForce, returnForce, cushionForce;
    public float JumpForce, speed;
    public int resetDelay;

    private Animator anim;
    private Rigidbody2D rb;
    private Transform tr;
    private HitBox hBox;
    private Text scoreDisplay;
    private ParticleSystem pSystem;
    private Manager manager;

    public Animator road;
    public GameObject deathBurst;

    private int score, resetTimer;
    private bool jump, push, dead, started;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        hBox = GetComponentInChildren<HitBox>();
        scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        pSystem = GameObject.Find("Trail(Clone)").GetComponent<ParticleSystem>();
        road = GameObject.Find("Road").GetComponent<Animator>();
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        Physics2D.IgnoreLayerCollision(8, 8, true);
        score = 0;
        resetTimer = 0;
    }

    
    private void Update()
    {
        if (started)
        {
            GetControls();
        }
        else
        {
            if(rb.velocity.y == 0)
            {
                started = true;
                speed = 1;
            }
        }
        if (resetTimer > resetDelay && (jump || push))
        {
            manager.Reset();
        }
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            ExecuteControls();
            ResetPosition();
        }
        else
        {
            resetTimer++;
        }
        score += (int)(speed * speed * speed);
        scoreDisplay.text = score.ToString();
    }

    private void GetControls()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("space") || Input.GetKeyDown("up"))
        {
            jump = true;
        }
        if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            push = true;
        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp("space") || Input.GetKeyUp("up"))
        {
            jump = false;
        }
        if (Input.GetKeyUp("d") || Input.GetKeyUp("right"))
        {
            push = false;
        }
    }

    private void ExecuteControls()
    {
        if (jump)
        {
            jump = false;
            push = false;
            anim.SetTrigger("jump");
        }
        if (push)
        {
            push = false;
            anim.SetTrigger("push");
        }
    }

    private void ResetPosition()
    {
        if(tr.position.x > -18)
        {
            if (tr.position.x > -17)
            {
                rb.AddForce(returnForce * (17 + tr.position.x) * Vector2.left, ForceMode2D.Force);
            }
            else
            {
                float s = rb.velocity.x;
                if (s < 0)
                {
                    rb.AddForce(cushionForce * Mathf.Abs(s) * Vector2.right, ForceMode2D.Force);
                }
            }
        }
    }


    private void PushForward()
    {
        if (!dead)
        {
            rb.AddForce(pushForce * Vector2.right, ForceMode2D.Impulse);
            speed *= 1.05f;
            road.SetFloat("speed", speed);
        }
    }

    private void Jump()
    {
        if (!dead)
        {
            hBox.Jump();
            rb.AddForce(JumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    public void GetHit()
    {
        if (!dead)
        {
            manager.OnDeath();
            Instantiate(deathBurst, tr.position, Quaternion.identity);
            Debug.Log("Get Smacked Nerd");
            dead = true;
            speed = 0;
            road.SetFloat("speed", speed);
            anim.SetTrigger("fall");
            rb.velocity = new Vector2(0, -50);
        }
    }
}
