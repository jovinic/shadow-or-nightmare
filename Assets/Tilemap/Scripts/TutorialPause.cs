using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPause : MonoBehaviour
{
    public GameObject player;
    public float waitTBearSeconds;

    private bool objectBlocked = false;
    private int tBearCount = 0;
    private GameObject tutorialTBear;

    public GameObject tutorialInput;

    void Update()
    {
        if(!objectBlocked)
        {
            return;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            tBearCount++;

            if(tBearCount == 1) // only threw, trigger retrieve tutorial
            {
                StartCoroutine(WaitForTBearRetrieve());
            }
        }

        if(tBearCount == 2) // threw and retrieved
        {
            destroySelf();
        }
    }

    IEnumerator WaitForTBearRetrieve()
    {
        yield return new WaitForSeconds(waitTBearSeconds);

        GameObject tutorialTBear = GameObject.FindGameObjectWithTag("TBear");

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
        if(other.tag == player.tag && !objectBlocked)
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
        player.GetComponent<Movement>().UnfreezePlayer();

        Destroy(this.gameObject);
    }

}
