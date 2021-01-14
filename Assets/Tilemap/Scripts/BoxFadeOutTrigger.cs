using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFadeOutTrigger : MonoBehaviour
{
    public void FadeOutAnimTrigger()
    {
        GetComponent<AudioSource>().Play();
        Transform fadeOutCanvas = transform.parent.Find("WhiteFlashCanvas");
        fadeOutCanvas.GetComponentInChildren<Animator>().SetTrigger("FadeOut");
    }
}
