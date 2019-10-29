using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private RigidbodyConstraints2D playerConstraints;
    private AnimationScript anim;

    [Space]
    [Header("Stats")]
    public int playerLevel = 0;
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallJumped;
    public bool wallSlide;

    [Space]
    private bool groundTouch;

    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    [Space]
    [Header("TBear Control")]
    public GameObject tBear;
    public GameObject ownTBear;
    [HideInInspector] public GameObject newTBear;
    public bool canThrow;

    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        playerConstraints = rb.constraints;
        anim = GetComponentInChildren<AnimationScript>();
    }

    void Update()
    {
        ownTBear.SetActive(canThrow ? true : false);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if(!canMove)
        {
            x = 0; y = 0;
        }

        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        anim.SetHorizontalMovement(x, y, rb.velocity.y);

        if(coll.triggerPush)
        {
            WallJump(true);
        }

        if(coll.onGround)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }

        rb.gravityScale = 3;

        if(coll.onWall && !coll.onGround)
        {
            if (x != 0)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (Input.GetButtonDown("Jump") && canMove)
        {
            anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump(false);
        }

        if (Input.GetButtonDown("Fire1") && canThrow && (playerLevel > 0))
        {
            if(coll.onWall && !coll.onGround)
            {
                //workaround to prevent player from climbing out of stage
            }
            else
            {
                anim.PrepareThrow();
            }
        }

        if (Input.GetButtonDown("Fire2") && !canThrow && (playerLevel > 0))
        {
            TBearRetrieve();
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallSlide || !canMove)
            return;

        FlipAnim(x);
    }

    public void TBearThrow()
    {
        canThrow = false;

        newTBear = Instantiate(tBear, transform.position, tBear.transform.rotation);
    }

    public void TBearRetrieve()
    {
        newTBear = null;
        canThrow = true;
    }

    public void TeleportPlayer(Transform destination)
    {
        transform.rotation = destination.rotation;
        transform.position = destination.position;
    }

    public void FreezePlayer(bool tutorialPause = false)
    {
        if (tutorialPause)
        {
            playerLevel = 1;
        }

        canMove = false;
        playerConstraints = rb.constraints;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnfreezePlayer()
    {
        rb.constraints = playerConstraints;
        canMove = true;
    }

    void GroundTouch()
    {

        side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
    }

    private void WallJump(bool pushTrigger)
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        if(pushTrigger)
        {
            Jump((wallDir / 4f), true);
        }
        else
        {
            Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);
        }

        wallJumped = true;
    }

    private void WallSlide()
    {
        if(coll.wallSide != side)
         anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || vertical < 0)
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    private void FlipAnim(float x)
    {
        if(x > 0)
        {
            side = 1;
        }
        if(x < 0)
        {
            side = -1;
        }

        anim.Flip(side);
    }
}
