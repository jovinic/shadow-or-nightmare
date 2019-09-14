using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platform;
    public Transform[] points;
    public float moveSpeed;
    public int pointSelection;

    private Transform currentPoint;

    void Start()
    {
        currentPoint = points[pointSelection];
    }

    void Update()
    {
        platform.transform.position = Vector2.MoveTowards(platform.transform.position,
                                                          currentPoint.position,
                                                          Time.deltaTime * moveSpeed);

        if(platform.transform.position == currentPoint.position)
        {
            pointSelection++;

            if(pointSelection == points.Length)
            {
                pointSelection = 0;
            }

            currentPoint = points[pointSelection];
        }

    }
}
