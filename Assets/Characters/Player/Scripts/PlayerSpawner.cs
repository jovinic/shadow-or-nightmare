using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        spriteRenderer.sprite = null;

        GameObject newPlayer = Instantiate(player,
                                           transform.position,
                                           player.transform.rotation) 
                               as GameObject;
    }
}
