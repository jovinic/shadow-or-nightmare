using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleasedAnimalController : MonoBehaviour
{
    public string animalIdentifier;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = (Random.value > 0.5f);
        animator = GetComponent<Animator>();
        animator.SetTrigger(animalIdentifier);
    }
}
