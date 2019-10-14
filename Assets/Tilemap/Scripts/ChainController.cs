using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainController : MonoBehaviour
{
    private bool chainTriggered = false;

    public void StartChain(int chainIndex)
    {
        if(chainTriggered)
        {
            return;
        }
        chainTriggered = true;
        StartCoroutine(TriggerChain(chainIndex));
    }

    IEnumerator TriggerChain(int chainIndex)
    {
        Transform chainTransform = transform.Find("chain" + chainIndex);
        if(chainTransform != null)
        {
            Destroy(chainTransform.gameObject);

            yield return new WaitForSeconds(.1f);
            StartCoroutine(TriggerChain(chainIndex + 1));
            StartCoroutine(TriggerChain(chainIndex - 1));
        }

        Transform boxTransform = transform.Find("box");
        Transform boxTransformChild = boxTransform.Find("interactBody");
        boxTransformChild.gameObject.GetComponent<PendulumInteract>().isHanging = false;
    }
}
