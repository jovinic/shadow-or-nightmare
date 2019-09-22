using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{

    private Animator anim;
    private Animator tbearAnim;
    private Movement move;
    private Collision coll;
    [HideInInspector]
    public SpriteRenderer sr;
    public SpriteRenderer tbearSr;

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<Collision>();
        move = GetComponentInParent<Movement>();
        tbearAnim = GetComponentInParent<Movement>().ownTBear.GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tbearSr = GetComponentInParent<Movement>().ownTBear.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        anim.SetBool("onGround", coll.onGround);
        tbearAnim.SetBool("onGround", coll.onGround);

        anim.SetBool("onWall", coll.onWall);
        tbearAnim.SetBool("onWall", coll.onWall);

        anim.SetBool("onRightWall", coll.onRightWall);
        tbearAnim.SetBool("onRightWall", coll.onRightWall);

        anim.SetBool("wallSlide", move.wallSlide);
        tbearAnim.SetBool("wallSlide", move.wallSlide);

        anim.SetBool("canMove", move.canMove);
        tbearAnim.SetBool("canMove", move.canMove);
    }

    public void SetHorizontalMovement(float x,float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        tbearAnim.SetFloat("HorizontalAxis", x);

        anim.SetFloat("VerticalAxis", y);
        tbearAnim.SetFloat("VerticalAxis", y);

        anim.SetFloat("VerticalVelocity", yVel);
        tbearAnim.SetFloat("VerticalVelocity", yVel);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
        tbearAnim.SetTrigger(trigger);
    }

    public void PrepareThrow()
    {
        anim.SetTrigger("throw");
        tbearAnim.SetTrigger("throw");
    }

    public void TriggerThrow()
    {
        move.TBearThrow();
    }

    public void Flip(int side)
    {

        if (move.wallSlide)
        {
            if (side == -1 && sr.flipX)
                return;

            if (side == 1 && !sr.flipX)
            {
                return;
            }
        }

        bool state = (side == 1) ? false : true;
        sr.flipX = state;
        tbearSr.flipX = state;
    }
}
