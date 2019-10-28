using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleasedAnimalController : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    private float moveSpeed;

    public float maxHeight;
    private Vector3 nextPosition;
    private Vector3 currentPosition;
    void Start()
    {
        currentPosition = transform.position;
        nextPosition = new Vector3(transform.position.x, maxHeight, transform.position.z);
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                                                 nextPosition,
                                                 Time.deltaTime * moveSpeed);

        if(transform.position == nextPosition)
        {
            nextPosition = currentPosition;
            currentPosition = transform.position;
        }
    }
}
