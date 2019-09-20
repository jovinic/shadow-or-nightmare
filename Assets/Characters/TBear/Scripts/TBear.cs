using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBear : MonoBehaviour
{
    public float initialTimer;
    private float initialGravity;
    public float colDelay;
    public float throwPower;
    public float minVelocity;

    public ParticleSystem burstParticle;
    public ParticleSystem idleParticle;

    private Rigidbody2D bearBody;
    private Vector3 mouseDir;
    private Vector3 velocity;
    private int availableBounces;
    private float currentTimer;

    private int playerLevel;

    void Start()
    {
        bearBody = GetComponent<Rigidbody2D>();
        currentTimer = initialTimer;
        initialGravity = bearBody.gravityScale;

        playerLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().playerLevel;
        availableBounces = playerLevel > 1 ? 1 : 0;

        Throw();
        burstParticle.Play();
        idleParticle.Play();
    }

    void Update()
    {
        currentTimer -= Time.deltaTime;

        // fixes direction
        Vector2 currentVelocity = bearBody.velocity;
        if (currentVelocity.magnitude >= minVelocity)
        {
            float targetAngle = Mathf.Atan2(currentVelocity.y, currentVelocity.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), throwPower * Time.deltaTime);
        }

        if(currentTimer <= 0)
        {
            bearBody.velocity = new Vector2(bearBody.velocity.x - (bearBody.velocity.x * 0.01f), bearBody.velocity.y);
            bearBody.gravityScale = initialGravity * 2f;
        }

        if(bearBody.velocity.magnitude > 0.1)
        {
            if(!idleParticle.isPlaying)
            {
                idleParticle.Play();
            }
        }
        else
        {
            if(idleParticle.isPlaying)
            {
                idleParticle.Stop();
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 bearPos = gameObject.GetComponent<Transform>().position;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(bearPos.x, bearPos.y + 0.15f, bearPos.z);

            destroyTBear(true);
            return;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            destroyTBear(true);
            return;
        }
    }

    public void Throw()
    {
        Vector3 cameraCenter = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.position.z);
        cameraCenter = Camera.main.ScreenToWorldPoint(cameraCenter);

        Vector3 throwDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - cameraCenter).normalized;
        throwDir.z = 1f;

        velocity = throwDir * throwPower;
        bearBody.AddForce(velocity);
    }

    void destroyTBear(bool destructor)
    {
        if(destructor)
        {
            Destroy(gameObject);
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().canThrow = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (availableBounces == 0 || currentTimer <= 0)
        {
            return;
        }

        bearBody.velocity = Vector2.zero;

        //obtain the surface normal for a point on a collider
        //and reflects a vector off the plane defined by a normal.
        Vector2 CollisionNormal = collision.contacts[0].normal;
        velocity = Vector3.Reflect(velocity, CollisionNormal);

        //apply new direction adding force
        velocity.Normalize();
        bearBody.AddForce(velocity * throwPower);

        // resets timer
        currentTimer = initialTimer * 0.6f;
        availableBounces--;
    }

    public void FreezeTBear()
    {
        bearBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
