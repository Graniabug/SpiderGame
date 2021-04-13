using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    public string enterTrigger;
    public string exitTrigger;

    public bool enemyEncounter;

    public string encounterTriggerEnter;
    public string encounterTriggerExit;

    public Animator camAnimator;
    public Animator enemyAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            camAnimator.SetTrigger(enterTrigger);
            camAnimator.ResetTrigger(exitTrigger);

            if(enemyEncounter == true) //if this is also an enemy encounter
            {
                enemyAnimator.SetTrigger(encounterTriggerEnter);
                enemyAnimator.ResetTrigger(encounterTriggerExit);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            camAnimator.SetTrigger(exitTrigger);
            camAnimator.ResetTrigger(enterTrigger);

            if(enemyEncounter == true)
            {
                enemyAnimator.SetTrigger(encounterTriggerExit);
                enemyAnimator.ResetTrigger(encounterTriggerEnter);
            }
        }
    }
}
