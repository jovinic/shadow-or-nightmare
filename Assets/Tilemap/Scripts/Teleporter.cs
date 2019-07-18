using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public Animator transitionAnim;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(TeleportPlayer(other.gameObject)); 
        }
    }

    IEnumerator TeleportPlayer(GameObject player)
    {
        player.GetComponent<Movement>().FreezePlayer();

        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(transitionAnim.GetCurrentAnimatorStateInfo(0).length);
        
        player.GetComponent<Movement>().TeleportPlayer(destination);
        player.GetComponent<Movement>().UnfreezePlayer();
    }
}
