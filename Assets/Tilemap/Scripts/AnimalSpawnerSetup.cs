using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnerSetup : MonoBehaviour
{
    public Transform spawnBegin;
    public Transform spawnEnd;
    public List<GameObject> animals;
    private int partitionCount;

    [Space]
    [Header("Endgame Dialogue Related")]
    public GameObject tutorialInput;
    private bool dialogueEnabled;
    public GameObject dialogueManager;

    void Start()
    {
        enabled = false;
    }

    void Update()
    {
        float axis = Input.GetAxis ("Vertical");
        if(axis > 0)
        {
            if(dialogueEnabled)
            {
                dialogueEnabled = false;

                GameObject backgroundAudio = GameObject.Find("BackgroundAudio");
                Destroy(backgroundAudio);
                dialogueManager.GetComponent<DialogueTrigger>().canBegin = true;
            }
        }
    }

    public void SpawnAnimals()
    {
        GameObject player = GameObject.FindWithTag("Player");

        GameObject activatedTBear = GameObject.FindWithTag("TBear");
        if(activatedTBear != null)
        {
            Destroy(activatedTBear);
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

        Transform boxTransform = transform.Find("box");
        Vector3 hintPosition = new Vector3(boxTransform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);

        GameObject newTutorialInput = Instantiate(tutorialInput, hintPosition, player.transform.rotation);
        newTutorialInput.GetComponent<TutorialInput>().buttonAnimTrigger = "Up";

        dialogueEnabled = true;
        enabled = true;
    }
}
