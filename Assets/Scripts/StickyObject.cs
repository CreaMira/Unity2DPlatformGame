using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyObject : MonoBehaviour
{

    //OnCollisionEnter2D(Collision2D)
    //OnTriggerEnter2D(Collider2D)
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.transform.SetParent(null);
        }
    }
}
