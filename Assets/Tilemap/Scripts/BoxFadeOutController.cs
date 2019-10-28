using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFadeOutController : MonoBehaviour
{
    public void DestroyBoxTrigger()
    {
        Destroy(transform.parent.parent.Find("box").gameObject);
    }

    public void SpawnAnimalsTrigger()
    {
        transform.parent.parent.GetComponent<AnimalSpawnerSetup>().SpawnAnimals();
    }
}
