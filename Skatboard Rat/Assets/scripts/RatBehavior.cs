using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatBehavior : MonoBehaviour
{
    public float pushForce, returnForce, cushionForce;
    public float JumpForce, speed, brakeForce, deathSlideSpeed, speedDecayFactor, scoreMultiplier;
    public int deathSlideDelay, score;

    private Animator anim;
    private Rigidbody2D rb;
    private Transform tr;
    private HitBox hBox;
    private Text scoreDisplay;
    private Manager manager;

    public Animator road;
    public GameObject deathBurst;

    private float realSpeed, realScore;
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
                Destroy(rb);
                //rb.constraints = RigidbodyConstraints2D.FreezeAll;
                //rb.Sleep();
                Destroy(GetComponent<BoxCollider2D>());
                //GetComponent<BoxCollider2D>().enabled = false;
                road.SetFloat("speed", deathSlideSpeed);
            }
            tr.position = new Vector2(tr.position.x - 1.8f, tr.position.y);
            if(tr.position.x < -40)
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
        //speed = realSpeed;
        //float v = rb.velocity.x/50;
        //speed += v;
        speed += (realSpeed - speed) / 10;
        if (!dead)
        {
            road.SetFloat("speed", realSpeed);
            if (started && speed >= 1)
            {
                Debug.Log("Speed: " + speed + "X: " + tr.position.x);
                tr.position = new Vector2(-49 + speed * 20, tr.position.y);
            }
            ExecuteControls();
            ResetPosition();
        }
        else
        {
            resetTimer++;
        }
        realScore += speed * speed * speed * scoreMultiplier;
        score = (int)realScore;
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
            realSpeed /= brakeForce;
            //rb.AddForce(brakeForce * Vector2.left);
        }
    }

    private void ResetPosition()
    {
        if(tr.position.x > -29)
        {
            realSpeed /= speedDecayFactor;
            //if (tr.position.x > -27)
            //{
            //    rb.AddForce(returnForce * (27 + tr.position.x) * Vector2.left, ForceMode2D.Force);
            //}
            //else
            //{
            //    float s = rb.velocity.x;
            //    if (s < 0)
            //    {
            //        rb.AddForce(cushionForce * Mathf.Abs(s) * Vector2.right, ForceMode2D.Force);
            //    }
            //}
        }
    }


    private void PushForward()
    {
        if (!dead)
        {
            //rb.AddForce(pushForce * Vector2.right, ForceMode2D.Impulse);
            if (realSpeed == 0)
            {
                realSpeed = 1;
            }
            realSpeed *= pushForce;
            //speed = realSpeed;
            road.SetFloat("speed", realSpeed);
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
