using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    public string sceneName;

    public Transform activator = null;
    public bool activated;

    private Animator anim;
    public Animator transitionAnim;

    private GameObject player;

    void Start()
    {
        activated = activator == null ? true : false;
        anim = GetComponentInParent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && activated)
        {
            StartCoroutine(TeleportToScene(other.gameObject));
        }
    }

    IEnumerator TeleportToScene(GameObject player)
    {
        player.GetComponent<Movement>().FreezePlayer();

        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(transitionAnim.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene(sceneName);
    }

    /* Activation Animations and Actions */

    public void ActivationAnimTrigger()
    {
        anim.SetTrigger("Open");
    }

    public void ActivationAnimBegin()
    {
        player.GetComponent<Movement>().FreezePlayer();
    }

    public void ActivationAnimEnd()
    {
        activated = true;
        player.GetComponent<Movement>().UnfreezePlayer();
    }
}
