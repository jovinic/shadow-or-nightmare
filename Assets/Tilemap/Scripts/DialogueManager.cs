using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    private Queue<string> sentences;

    public Animator animator;
    public Animator transitionAnim;
    public string nextSceneName;
    public AudioSource clickAudio;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        clickAudio.Play();

        if(sentences.Count == 0)
        {
            EndDialogue();
            StartCoroutine(LoadSceneAfterDialogue(nextSceneName));
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }

    IEnumerator LoadSceneAfterDialogue(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(transitionAnim.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene(sceneName);
    }

}
