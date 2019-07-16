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
            other.gameObject.GetComponent<Movement>().FreezePlayer(true);

            playerBlocked = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player" && playerBlocked)
        {
            other.gameObject.GetComponent<Movement>().UnfreezePlayer();
        }
    }
}
