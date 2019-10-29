using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnerSetup : MonoBehaviour
{
    public Transform spawnBegin;
    public Transform spawnEnd;
    public List<GameObject> animals;
    private int partitionCount;

    public void SpawnAnimals()
    {
        GameObject player = GameObject.FindWithTag("Player");

        GameObject activatedTBear = GameObject.FindWithTag("TBear");
        if(activatedTBear != null)
        {
            Destroy(activatedTBear);
            player.GetComponent<Movement>().TBearRetrieve();
        }

        player.GetComponent<Movement>().canThrow = false;
        player.GetComponent<Movement>().playerLevel = 0;


        float xPartition = (spawnEnd.position.x - spawnBegin.position.x) / animals.Count;
        partitionCount = 0;

        foreach (GameObject animal in animals)
        {
            Vector3 randomPosition =
                        new Vector3(spawnBegin.position.x + (xPartition * partitionCount),
                                    spawnEnd.position.y,
                                    0);
            GameObject newAnimal = Instantiate(animal, randomPosition, transform.rotation);

            partitionCount++;
        }
    }
}
