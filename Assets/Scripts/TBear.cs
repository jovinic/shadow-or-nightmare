using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBear : MonoBehaviour
{
    public float timer;
    public float colDelay;

    void Start()
    {        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
            
            destroyTBear();
            return;
        }

        if(timer <= 0)
        {
            destroyTBear();
        }
    }

    void destroyTBear()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canThrow = true;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag != "Player")
        {
            //Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), gameObject.GetComponent<Collider>());            
            timer = 0;
        }
    }
}
