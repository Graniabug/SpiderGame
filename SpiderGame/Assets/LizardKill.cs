using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            other.gameObject.SetActive(false);
        }
    }
}
