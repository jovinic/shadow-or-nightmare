using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator animator;
    public float speed;
    private Vector3 originalScale;
    public GameObject tBear;
    public float throwPower;
    public bool canThrow;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
        canThrow = true;
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        Throw();
    }

    void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            myBody.velocity = new Vector2(speed * h, myBody.velocity.y);

            Vector3 scale = transform.localScale;
            scale.x = h > 0 ? originalScale.x : -originalScale.x;
            transform.localScale = scale;

            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }        
    }

    void Throw()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!canThrow)
                return;                

            canThrow = false;
            animator.SetTrigger("Throw");

            GameObject newTBear = Instantiate(tBear,
                                              transform.position,
                                              tBear.transform.rotation);
            
            // throw towards mouse position
            Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = (Input.mousePosition - sp).normalized;
            newTBear.GetComponent<Rigidbody2D>().AddForce(dir * throwPower);
            
            // set scale for object position (todo)
            
        }
    }
}