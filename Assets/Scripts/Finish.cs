using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool flag = false;
    // Start is called before the first frame update
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player") && !flag)
        {
            flag = true;
            finishSound.Play();
            Invoke("CompleteLevel", 2f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
