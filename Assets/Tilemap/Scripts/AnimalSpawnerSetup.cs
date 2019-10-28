using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnerSetup : MonoBehaviour
{
    public List<GameObject> animals;
    public Transform spawnBegin;
    public Transform spawnEnd;

    public void SpawnAnimals()
    {
        foreach (GameObject animal in animals)
        {
            Vector3 randomPosition =
                        new Vector3(Random.Range(spawnBegin.position.x, spawnEnd.position.x),
                        spawnEnd.position.y,
                        0);
            GameObject newAnimal = Instantiate(animal, randomPosition, transform.rotation);
            newAnimal.GetComponent<ReleasedAnimalController>().maxHeight = spawnBegin.position.y;
        }
    }
}
