using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ItemCollector : MonoBehaviour
{
    public TMP_Text collectionText;
    private int numCollection = 0;

    [SerializeField] private AudioSource collectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Collection"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            numCollection++;

            collectionText.text = "Collections: " + numCollection;
            
            Debug.Log("Collections: " + numCollection);
        }
    }



}
