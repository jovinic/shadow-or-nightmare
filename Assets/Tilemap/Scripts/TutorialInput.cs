using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInput : MonoBehaviour
{
    public string buttonAnimTrigger;
    private Animator anim;
    private bool animTriggered = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(buttonAnimTrigger != null && !animTriggered)
        {
            animTriggered = true;
            anim.SetTrigger(buttonAnimTrigger);

            switch(buttonAnimTrigger)
            {
                case "WalkRight":
                    StartCoroutine(WaitForWalkInput());
                    break;
                case "LMB":
                    StartCoroutine(WaitForLMBInput());
                    break;
                case "Up":
                    StartCoroutine(WaitForUpInput());
                    break;
            }
        }
    }

    IEnumerator WaitForWalkInput()
    {
        float axis = 0;
        while(axis == 0)
        {
            axis = Input.GetAxis ("Horizontal");
            yield return null;
        }

        Destroy(this.gameObject);
    }

    IEnumerator WaitForUpInput()
    {
        float axis = 0;
        while(axis <= 0)
        {
            axis = Input.GetAxis ("Vertical");
            yield return null;
        }

        Destroy(this.gameObject);
    }

    IEnumerator WaitForLMBInput()
    {
        while(!Input.GetButtonDown("Fire1"))
        {
            yield return null;
        }

        Destroy(this.gameObject);
    }

}
