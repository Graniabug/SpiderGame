using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttack2 : MonoBehaviour
{
    public Animator camAnimator; //grab the camera animator
    public Animator birdAnimator;

    public Animator birdEnemy;
    //public AudioSource attackSound;
    public AudioSource leaveSound;

    private bool leavehasPlayed = false;

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            birdEnemy.SetTrigger("Flying");
            camAnimator.SetTrigger("ScriptedAttack2START");
            birdAnimator.SetTrigger("attack");
            birdEnemy.ResetTrigger("Flying");
            //attackSound.Play();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            birdEnemy.SetTrigger("Attack");
            camAnimator.ResetTrigger("ScriptedAttack2START");
            camAnimator.SetTrigger("ScriptedAttack2END");
        }
    }
}
