using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatBehavior : MonoBehaviour
{
    public float pushForce, returnForce, cushionForce;
    public float JumpForce, speed, brakeForce;
    public int deathSlideDelay, score;

    private Animator anim;
    private Rigidbody2D rb;
    private Transform tr;
    private HitBox hBox;
    private Text scoreDisplay;
    private Manager manager;

    public Animator road;
    public GameObject deathBurst;

    private float realSpeed;
    private int resetTimer;
    private bool jump, push, brake, dead, started, canReset;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        hBox = GetComponentInChildren<HitBox>();
        scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        road = GameObject.Find("Road").GetComponent<Animator>();
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        Physics2D.IgnoreLayerCollision(8, 8, true);
        speed = 0;
        realSpeed = 0;
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
            }
        }
        if(resetTimer >= deathSlideDelay)
        {
            if(resetTimer == deathSlideDelay)
            {
                rb.Sleep();
                GetComponent<BoxCollider2D>().enabled = false;
                road.SetFloat("speed", 1f);
            }
            tr.position = new Vector2(tr.position.x - 0.2f, tr.position.y);
            if(tr.position.x < -30)
            {
                road.SetFloat("speed", 0);
                canReset = true;
            }
        }
        if (canReset && (jump || push))
        {
            manager.Reset();
        }
    }

    private void FixedUpdate()
    {
        speed = realSpeed;
        float v = rb.velocity.x/50;
        speed += v;
        Debug.Log("Speed = " + realSpeed);
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
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            brake = true;
        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp("space") || Input.GetKeyUp("up"))
        {
            jump = false;
        }
        if (Input.GetKeyUp("d") || Input.GetKeyUp("right"))
        {
            push = false;
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("left"))
        {
            brake = false;
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
        if (brake)
        {
            rb.AddForce(brakeForce * Vector2.left);
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
            if(realSpeed == 0)
            {
                realSpeed = 1;
            }
            realSpeed *= 1.05f;
            speed = realSpeed;
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
            realSpeed = 0;
            road.SetFloat("speed", speed);
            anim.SetTrigger("fall");
            rb.velocity = new Vector2(0, -50);
        }
    }
}
