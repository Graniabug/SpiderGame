using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    public string enterTrigger;
    public string exitTrigger;

    public Animator camAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            camAnimator.SetTrigger(enterTrigger);
            camAnimator.ResetTrigger(exitTrigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            camAnimator.SetTrigger(exitTrigger);
            camAnimator.ResetTrigger(enterTrigger);
        }
    }
}
