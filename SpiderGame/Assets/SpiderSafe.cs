using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSafe : MonoBehaviour
{
    public SpiderMove player;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            player.safe = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            player.safe = false;
        }

        
    }
}
