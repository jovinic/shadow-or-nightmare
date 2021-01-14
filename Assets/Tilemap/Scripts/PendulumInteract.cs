using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumInteract : MonoBehaviour
{
    public GameObject tutorialInput;
    public bool isHanging = true;
    private Animator anim;
    private GameObject player;
    private bool triggerHint = false;
    private bool playerTrigger = false;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if(isHanging)
        {
            return;
        }

        float axis = Input.GetAxis ("Vertical");
        if(axis > 0)
        {
            if(playerTrigger)
            {
                ActivationAnimTrigger();
            }
            else
            {
                triggerHint = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !isHanging)
        {
            playerTrigger = true;

            if(player == null)
            {
                player = other.gameObject;
            }

            if(!triggerHint)
            {
                Vector3 hintPosition = transform.position +
                                       new Vector3(0.0f, 1.5f, 0.0f);
                GameObject newTutorialInput = Instantiate(tutorialInput, hintPosition, player.transform.rotation);
                newTutorialInput.GetComponent<TutorialInput>().buttonAnimTrigger = "Up";
                triggerHint = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player" && !isHanging)
        {
            playerTrigger = false;
        }
    }

    public void ActivationAnimTrigger()
    {
        GetComponent<AudioSource>().Play();
        anim.SetTrigger("Activate");
    }
}
