using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerPhysics 
{
    public float speedMultiplier = 7;
    public float jumpTakeOffSpeed = 7;        

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> (); 
        animator = GetComponent<Animator> ();
    }   

    protected override void Throw()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!canThrow)
                return;

            canThrow = false;
            animator.SetTrigger("Throw");

            GameObject newTBear = Instantiate(tBear,
                                              transform.position,
                                              tBear.transform.rotation) 
                                  as GameObject;
        }
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if(move.x > 0.01f)
        {
            if(spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        } 
        else if (move.x < -0.01f)
        {
            if(spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }

        animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / speedMultiplier);

        targetVelocity = move * speedMultiplier;
    }
}