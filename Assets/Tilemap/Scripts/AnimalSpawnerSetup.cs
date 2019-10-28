using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnerSetup : MonoBehaviour
{
    public GameObject releasedAnimalGenerator;
    public List<string> animals;
    public Transform spawnBegin;
    public Transform spawnEnd;

    public void SpawnAnimals()
    {
        foreach (string animal in animals)
        {
            Vector3 randomPosition =
                        new Vector3(Random.Range(spawnBegin.position.x, spawnEnd.position.x),
                        spawnEnd.position.y,
                        0);
            GameObject newAnimal = Instantiate(releasedAnimalGenerator, randomPosition, transform.rotation);
            newAnimal.GetComponent<ReleasedAnimalController>().maxHeight = spawnBegin.position.y;
            newAnimal.GetComponent<Animator>().SetTrigger(animal);
        }
    }
}
