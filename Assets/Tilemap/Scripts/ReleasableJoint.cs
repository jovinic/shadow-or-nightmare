﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleasableJoint : MonoBehaviour
{
    public int orderSeq;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "TBear")
        {
            transform.parent.gameObject.GetComponent<ChainController>().StartChain(orderSeq);
        }
    }
}
