﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBear : MonoBehaviour
{
    public float initialTimer;
    public float colDelay;
    public float throwPower;
    
    private Rigidbody2D bearBody;
    private Vector3 mouseDir;  
    private Vector3 velocity;
    private int availableBounces;
    private float currentTimer;

    private int playerLevel;

    void Start()
    {        
        bearBody = GetComponent<Rigidbody2D>();
        currentTimer = initialTimer;

        playerLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().playerLevel;
        availableBounces = playerLevel >= 2 ? 1 : 0;
        
        Throw();
    }

    void Update()
    {
        currentTimer -= Time.deltaTime;

        if(availableBounces == 0)
        {
            //bearBody.velocity = new Vector2(bearBody.velocity.x * 0.9f, bearBody.velocity.y * 0.9f);
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
            
            destroyTBear(true);
            return;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            destroyTBear(true);
            return;
        }

        // if (currentTimer <= 0)
        // {
        //     destroyTBear();
        // }
    }

    public void Throw()
    {
        // throw towards mouse position
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        mouseDir = (Input.mousePosition - sp).normalized;
        velocity = mouseDir * throwPower;
        bearBody.AddForce(velocity);
        
        // set scale for object position (todo)
        Vector3 moveDirection = Vector3.right;
        Vector3 moveToward;
        // gets the mouse position relative to the camera and stores it in movetoward
        moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        // moveDirection (variable announced at top of class) becomes moveToward - current position
        moveDirection = moveToward - transform.position;
        // make z part of the vector 0 as we dont need it
        moveDirection.z = 0;
        // normalize the vector so its in units of 1
        moveDirection.Normalize();
        // if we have moved and need to rotate
        if (moveDirection != Vector3.zero)
        {
            // calculates the angle we should turn towards, - 90 makes the sprite rotate
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
            // actually rotates the sprite using Slerp (from its previous rotation, to the new one at the designated speed.
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), throwPower * Time.deltaTime);
        }
    }

    void destroyTBear(bool destructor)
    {
        if(destructor)
        {
            Destroy(gameObject);
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().canThrow = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (availableBounces == 0)
        {
            return;
        }
        
        bearBody.velocity = Vector2.zero;

        //obtain the surface normal for a point on a collider 
        //and reflects a vector off the plane defined by a normal.        
        Vector2 CollisionNormal = collision.contacts[0].normal;        
        velocity = Vector3.Reflect(velocity, CollisionNormal);

        //apply new direction adding force
        velocity.Normalize();
        bearBody.AddForce(velocity * throwPower);

        // fixes direction
        float targetAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), throwPower * Time.deltaTime);

        // resets timer
        currentTimer = initialTimer * 0.6f;
        availableBounces--;  
    }
}
