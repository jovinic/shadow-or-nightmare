using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBear : MonoBehaviour
{
    public float timer;
    public float colDelay;

    public float throwPower;

    void Start()
    {        
        Throw();
    }

    public void Throw()
    {
        // throw towards mouse position
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * throwPower);
        
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

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
            
            destroyTBear();
            return;
        }

        if(timer <= 0)
        {
            destroyTBear();
        }
    }

    void destroyTBear()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPhysics>().canThrow = true;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;
        //Instantiate(explosionPrefab, pos, rot);
        //Destroy(gameObject);
        destroyTBear();
        transform.position = Vector3.Reflect(transform.position, Vector3.right);
    }
}
