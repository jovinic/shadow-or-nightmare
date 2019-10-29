using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleasedAnimalController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = (Random.value > 0.5f);
    }
}
