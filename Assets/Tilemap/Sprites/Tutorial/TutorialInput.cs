using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInput : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForWalkInput());        
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
}
