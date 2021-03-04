using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdKill2 : MonoBehaviour
{
    public GameObject deathText;
    public float speed = 5.0f;
    public Transform spider;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Spider")
        {
            if(other.gameObject.GetComponent<SpiderMove>().safe == false)
            {
                float step = speed * Time.deltaTime;
                spider = other.gameObject.transform;
                this.gameObject.GetComponent<Animator>().enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, spider.position, step);
                Destroy(other.gameObject);
                deathText.SetActive(true);
                this.gameObject.transform.position = other.transform.position;
            }

        }
    }
}
