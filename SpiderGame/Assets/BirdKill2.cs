using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdKill2 : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            Destroy(other.gameObject);
        }
    }
}
