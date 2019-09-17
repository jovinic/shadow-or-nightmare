using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageInteract : MonoBehaviour
{
    public GameObject bird;
    public GameObject destroyableW;
    public GameObject tutorialInput;
    public float birdSpeed;

    private Animator anim;
    private GameObject player;
    private bool hasBird = false;
    private bool triggerHint = false;
    private bool playerTrigger = false;
    private bool moveBird = false;

    void Start()
    {
        anim = GetComponentInParent<Animator>();

        if(bird != null)
        {
            hasBird = true;
        }
    }

    void Update()
    {
        if(moveBird && destroyableW != null)
        {
            bird.transform.position = Vector2.MoveTowards(bird.transform.position,
                                                          destroyableW.transform.position,
                                                          Time.deltaTime * birdSpeed);

            if(bird.transform.position == destroyableW.transform.position)
            {
                destroyableW.GetComponent<Animator>().SetTrigger("Fade");
            }
        }

        if(!hasBird)
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
        if(other.tag == "Player")
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
        if(other.tag == "Player")
        {
            playerTrigger = false;
        }
    }

    public void ActivationAnimTrigger()
    {
        anim.SetTrigger("Open");
    }

    public void BirdAnimTrigger()
    {
        if(!hasBird)
        {
            return;
        }

        bird.GetComponent<Animator>().SetTrigger("Leave");
        hasBird = false;
        moveBird = true;
    }
}
