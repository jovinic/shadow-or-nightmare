using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterColliderPush : MonoBehaviour
{
    public string exceptionObjTag;
    public float pushForce;

    private bool canPush = false;
    private Rigidbody2D colliderBody;

    void Update()
    {
        if(canPush)
        {

            print(colliderBody.velocity);
            //apply new direction adding force
            //velocity.Normalize();

            colliderBody.velocity = new Vector2(colliderBody.velocity.x, 0);
            colliderBody.velocity += Vector2.left * pushForce;
            print(colliderBody.velocity);

            colliderBody = null;
            canPush = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObj = collision.gameObject;

        if(collidedObj.tag == exceptionObjTag)
        {
            return;
        }
        else if (collidedObj.tag == "Player")
        {
            colliderBody = collidedObj.GetComponent<Rigidbody2D>();
            canPush = true;
        }

    }
}
