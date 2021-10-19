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
        GameObject.FindGameObjectWithTag("Player").GetComponent<SoundEffects>().playThrowAudio();
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
            // increase gravity effect to add weight to tbear
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

            GameObject.FindGameObjectWithTag("Player").GetComponent<SoundEffects>().playTeleportAudio();
            destroyTBear(true);
            return;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<SoundEffects>().playRetrieveAudio();

            destroyTBear(true);
            return;
        }
    }

    public void Throw()
    {
        Vector2 cursorInWorldPos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        Vector2 direction = cursorInWorldPos - bearBody.position;
        direction.Normalize();
        bearBody.AddForce(direction * throwPower);
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

       // buff force for bounce effect
       bearBody.AddForce(bearBody.velocity * (throwPower * 0.1f));

        // resets timer
        currentTimer = initialTimer * 0.6f;
        availableBounces--;

        GameObject.FindGameObjectWithTag("Player").GetComponent<SoundEffects>().playBounceAudio();
    }

    public void FreezeTBear()
    {
        bearBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
