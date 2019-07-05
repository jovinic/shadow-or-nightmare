using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D myBody;

    [Header("Physics Modifiers")]
    public float speedMultiplier = 7;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;    
    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    private Vector2 targetVelocity;
    private Vector2 velocity;
    private Vector2 groundNormal;

    private const float minMoveDistance = 0.001f;
    private const float shellRadius = 0.01f;

    private ContactFilter2D contactFilter;
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    private bool canMove;
    private bool grounded;
    
    [Header("TBear Control")]
    public GameObject tBear;
    [HideInInspector] public bool canThrow;
    
    void Awake() 
    {
        myBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
    }   

    void Start() 
    {
        Scene scene = SceneManager.GetActiveScene();
        canThrow = scene.name.Contains("001") ? false : true;     

        canMove = true;        
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Update() 
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();         

        Throw();
    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);        

        // limits speed
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
    }

    void Movement(Vector2 move, bool yMovement)
    {
        if (!canMove)
        {
            return;
        }
        
        float distance = move.magnitude;

        if (distance > minMoveDistance) 
        {
            int count = myBody.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++) 
            {
                hitBufferList.Add(hitBuffer [i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++) 
            {
                Vector2 currentNormal = hitBufferList [i].normal;
                if (currentNormal.y > minGroundNormalY) 
                {
                    grounded = true;
                    if (yMovement) 
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot (velocity, currentNormal);
                if (projection < 0) 
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList [i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        
        myBody.position = myBody.position + move.normalized * distance;
    }

    void Throw()
    {        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {            
            if (!canThrow)
            {
                return;
            }

            canThrow = false;
            animator.SetTrigger("Throw");

            GameObject newTBear = Instantiate(tBear,
                                              transform.position,
                                              tBear.transform.rotation) 
                                  as GameObject;
        }
    }

    void ComputeVelocity()
    {
        if (!canMove)
        {
            return;
        }            
        
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp("Jump")) 
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if (move.x > 0.01f)
        {
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        } 
        else if (move.x < -0.01f)
        {
            if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / speedMultiplier);

        targetVelocity = move * speedMultiplier;
    }
}