﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnerSetup : MonoBehaviour
{
    public GameObject releasedAnimalGenerator;
    public List<string> animals;
    public Transform spawnBegin;
    public Transform spawnEnd;
    private int partitionCount;

    public void SpawnAnimals()
    {
        float xPartition = (spawnEnd.position.x - spawnBegin.position.x) / animals.Count;
        partitionCount = 0;

        foreach (string animal in animals)
        {
            Vector3 randomPosition =
                        new Vector3(spawnBegin.position.x + (xPartition * partitionCount),
                                    spawnEnd.position.y,
                                    0);
            GameObject newAnimal = Instantiate(releasedAnimalGenerator, randomPosition, transform.rotation);
            newAnimal.GetComponent<Animator>().SetTrigger(animal);

            partitionCount++;
        }
    }
}
