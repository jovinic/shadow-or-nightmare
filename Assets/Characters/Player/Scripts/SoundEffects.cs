using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource jumpAudio;
    public AudioSource throwAudio;
    public AudioSource teleportAudio;
    public AudioSource retrieveAudio;
    public AudioSource bounceAudio;

    public void playJumpAudio()
    {
        jumpAudio.Play();
    }

    public void playThrowAudio()
    {
        throwAudio.Play();
    }

    public void playTeleportAudio()
    {
        teleportAudio.Play();
    }

    public void playRetrieveAudio()
    {
        retrieveAudio.Play();
    }

    public void playBounceAudio()
    {
        bounceAudio.Play();
    }
}
