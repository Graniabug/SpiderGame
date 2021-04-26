using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttack1 : MonoBehaviour
{
    public Animator camAnimator; //grab camera animator
    public Animator birdAnimator;
    public GameObject player; //grab player object
    public AudioSource warningSound; //grab audio for the bird's warning sound
    public AudioSource attackSound; //grab audio for the bird's attack sound
    Animator birdMovement;

    private void Start()
    {
        birdMovement = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other) //if the player enters the trigger at the begining
    {
        if(other.gameObject.name == "Spider")
        {
            camAnimator.SetTrigger("ScriptedAttack1"); //Move camera to new angle
            birdMovement.SetTrigger("ScriptedAttack");//move the bird
            birdAnimator.SetTrigger("Attack");
            birdAnimator.SetBool("FliesAway", true);
            warningSound.Play();
            StartCoroutine(PlayAttackSound());

            StartCoroutine(freezePlayer()); //disable player movement for a short time
        }
    }

    IEnumerator freezePlayer() //disable player movement for a short time
    {
        player.GetComponent<SpiderMove>().enabled = false;

        yield return new WaitForSeconds(5.0f);

        player.GetComponent<SpiderMove>().enabled = true;

        yield return null;
    }

    IEnumerator PlayAttackSound() //play attack sound a few seconds after the player started watching the bird
    {
        yield return new WaitForSeconds(2.0f);

        attackSound.Play();

        yield return null;
    }
}
