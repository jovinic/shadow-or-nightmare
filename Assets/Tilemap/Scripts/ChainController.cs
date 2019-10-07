using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainController : MonoBehaviour
{
    public List<int> destroyedChain = new List<int>();
    private bool isProcessingList = false;

    void Start()
    {

    }

    void Update()
    {
        if(destroyedChain.Count == 0 || isProcessingList)
        {
            return;
        }
        else
        {
            isProcessingList = true;
            foreach(int i in destroyedChain)
            {
                Transform chain = transform.Find("chain"+i);
                chain.gameObject.GetComponent<ReleasableJoint>().canDestroy = true;

                if ((i-1) > 0)
                {
                    destroyedChain.Add(i-1);
                }

                if ((i+1) <= destroyedChain.Count)
                {
                    destroyedChain.Add(i+1);
                }

                break;
            }
            isProcessingList = false;
        }
    }
}
