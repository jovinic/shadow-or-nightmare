using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPause : MonoBehaviour
{
    public GameObject player;
    public float waitTBearSeconds;

    private bool objectBlocked;
    private GameObject tutorialTBear;

    public GameObject tutorialInput;

    void Start()
    {
        objectBlocked = false;
    }

    void Update()
    {
        if(objectBlocked && Input.GetButtonDown("Fire1"))
        {
            if(tutorialTBear == null)
            {
                tutorialTBear = GameObject.FindGameObjectWithTag("TBear");

                StartCoroutine(WaitForTBearRetrieve());
            } else
            {
                destroySelf();
            }
        }
    }

    IEnumerator WaitForTBearRetrieve()
    {
        yield return new WaitForSeconds(waitTBearSeconds);

        if(tutorialTBear != null)
        {
            tutorialTBear.GetComponent<TBear>().FreezeTBear();
            Vector3 hintPosition = tutorialTBear.transform.position +
                                new Vector3(0.0f, 2.0f, 0.0f);
            GameObject newTutorialInput = Instantiate(tutorialInput, hintPosition, player.transform.rotation);
            newTutorialInput.GetComponent<TutorialInput>().buttonAnimTrigger = "LMB";
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == player.name && !objectBlocked)
        {
            player.GetComponent<Movement>().FreezePlayer(true);

            Vector3 hintPosition = player.transform.position +
                                   new Vector3(0.0f, 2.0f, 0.0f);
            GameObject newTutorialInput = Instantiate(tutorialInput, hintPosition, player.transform.rotation);
            newTutorialInput.GetComponent<TutorialInput>().buttonAnimTrigger = "LMB";

            objectBlocked = true;
        }
    }

    void destroySelf()
    {
        Destroy(gameObject);

        player.GetComponent<Movement>().UnfreezePlayer();
    }

}
