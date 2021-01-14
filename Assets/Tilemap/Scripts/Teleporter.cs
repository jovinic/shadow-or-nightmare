using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public Transform activator = null;
    public GameObject openLight = null;
    public bool activated;

    private Animator anim;
    public Animator transitionAnim;

    private GameObject player;

    void Start()
    {
        activated = activator == null ? true : false;
        anim = GetComponentInParent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        if(openLight != null)
        {
            openLight.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && activated)
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
        if(openLight != null)
        {
            openLight.SetActive(true);
        }
    }

    public void ActivationAnimEnd()
    {
        activated = true;
        player.GetComponent<Movement>().UnfreezePlayer();
    }
}
