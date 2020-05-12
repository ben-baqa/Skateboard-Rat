using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehavior : MonoBehaviour
{
    public float pushForce, returnForce, cushionForce;
    public float JumpForce, speed;

    private Animator anim;
    private Rigidbody2D rb;
    private Transform tr;
    private HitBox hBox;

    public Animator road;

    private bool jump, push;
    private bool contraintsReset;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        hBox = GetComponentInChildren<HitBox>();
        Physics2D.IgnoreLayerCollision(8, 8, true);
        speed = 1;
    }

    
    private void Update()
    {
        GetControls();
    }

    private void FixedUpdate()
    {
        ExecuteControls();
        ResetPosition();
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
            contraintsReset = false;
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
        else if(!contraintsReset)
        {
            //rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            //rb.constraints &= RigidbodyConstraints2D.FreezeRotation;
            //contraintsReset = true;
        }
    }


    private void PushForward()
    {
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(pushForce * Vector2.right, ForceMode2D.Impulse);
        speed *= 1.05f;
        road.SetFloat("speed", speed);
    }

    private void Jump()
    {
        hBox.Jump();
        rb.AddForce(JumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    public void GetHit()
    {
        Debug.Log("Get Smacked Nerd");
    }
}
