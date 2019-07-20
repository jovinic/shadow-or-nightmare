using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public Transform activator = null;
    public bool activated;
    
    public Animator transitionAnim;
    private Animator anim;

    private GameObject player;

    void Start()
    {
        activated = activator == null ? true : false;
        anim = GetComponentInParent<Animator>(); 
        player = GameObject.FindGameObjectWithTag("Player");
    }

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

    /* Activation Animations and Actions */
    
    public void ActivationAnimTrigger()
    {
        anim.SetTrigger("Open");
    }

    public void ActivationAnimBegin()
    {
        player.GetComponent<Movement>().FreezePlayer();
    }
    
    public void ActivationAnimEnd()
    {
        activated = true;
        player.GetComponent<Movement>().UnfreezePlayer();
    }
}
