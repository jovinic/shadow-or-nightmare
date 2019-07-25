using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPause : MonoBehaviour
{
    public GameObject player;
    public bool waitTBear;
    public float waitTBearSeconds;

    private bool objectBlocked;

    public GameObject tutorialInput;

    void Start()
    {
        objectBlocked = false;
    }

    void Update()
    {
        if(waitTBear && objectBlocked && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(WaitForTBearRetrieve());
            waitTBear = false;
        }
    }

    IEnumerator WaitForTBearRetrieve()
    {
        yield return new WaitForSeconds(waitTBearSeconds);

        player.GetComponentInChildren<TBear>().GetComponent<TBear>().FreezeTBear();
        Vector3 hintPosition = player.GetComponentInChildren<TBear>().transform.position +
                               new Vector3(0.0f, 2.0f, 0.0f);
        GameObject newTutorialInput = Instantiate(tutorialInput, hintPosition, player.transform.rotation);
        newTutorialInput.GetComponent<TutorialInput>().buttonAnimTrigger = "LMB";
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

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == player.name && objectBlocked)
        {
            player.GetComponent<Movement>().UnfreezePlayer();
        }
    }
}
