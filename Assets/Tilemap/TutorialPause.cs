using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPause : MonoBehaviour
{
    private bool playerBlocked;
    private RigidbodyConstraints2D playerConstraints;
    
    void Start()
    {
        playerBlocked = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !playerBlocked)
        {            
            other.gameObject.GetComponent<Movement>().playerLevel = 1;
            other.gameObject.GetComponent<Movement>().canMove = false;
            playerConstraints = other.gameObject.GetComponent<Rigidbody2D>().constraints;
            other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            playerBlocked = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player" && playerBlocked)
        {            
            other.gameObject.GetComponent<Movement>().canMove = true;
            other.gameObject.GetComponent<Rigidbody2D>().constraints = playerConstraints;
        }
    }
}
