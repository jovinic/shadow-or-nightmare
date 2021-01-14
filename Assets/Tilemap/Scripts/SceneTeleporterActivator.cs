using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleporterActivator : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !GetComponentInParent<SceneTeleporter>().activated)
        {
            GetComponentInParent<SceneTeleporter>().ActivationAnimTrigger();
            GetComponent<AudioSource>().Play();
        }
    }
}
