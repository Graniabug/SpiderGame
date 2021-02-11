using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttack2 : MonoBehaviour
{
    public Animator camAnimator; //grab the camera animator

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            camAnimator.SetTrigger("ScriptedAttack2START");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            camAnimator.ResetTrigger("ScriptedAttack2START");
            camAnimator.SetTrigger("ScriptedAttack2END");
        }
    }
}
