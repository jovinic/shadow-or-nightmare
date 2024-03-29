﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask triggerPushLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public bool triggerPush;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public GameObject sceneLimit;

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    void Update()
    {
        if(sceneLimit != null && transform.position.y < sceneLimit.transform.position.y)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        LayerMask collingLayers = groundLayer | triggerPushLayer;

        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, collingLayers);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, collingLayers) ||
                 Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, collingLayers);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, collingLayers);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, collingLayers);

        triggerPush = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, triggerPushLayer) ||
                 Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, triggerPushLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
