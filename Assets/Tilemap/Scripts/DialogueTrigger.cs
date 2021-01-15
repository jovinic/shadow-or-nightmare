using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool canBegin = false;
    public Dialogue dialogue;
    private bool dialogueStarted = false;

    void Update()
    {
        if(!canBegin)
        {
            return;
        }

        if(dialogueStarted)
        {
            return;
        }

        dialogueStarted = true;
        StartCoroutine(WaitDialogueDelay(1f));
    }

    IEnumerator WaitDialogueDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
