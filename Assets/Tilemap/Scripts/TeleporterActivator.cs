using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterActivator : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {      
        if(other.tag == "Player" && !GetComponentInParent<Teleporter>().activated)
        {
            GetComponentInParent<Teleporter>().ActivationAnimTrigger();
        }
    }
}
