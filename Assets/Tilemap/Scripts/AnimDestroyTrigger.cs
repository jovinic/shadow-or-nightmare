using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDestroyTrigger : MonoBehaviour
{
    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
