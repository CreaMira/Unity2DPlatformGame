using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeHead : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    private Rigidbody2D rb;


    [SerializeField] private bool UpDown = false;
    [SerializeField] private bool LeftRight = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Move spikehead to destination only if attacking
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        if(UpDown)
        {
            SetUPDown();
        }
        else if(LeftRight)
        {
            SetLeftRight();
        }
        else
        {
            SetFourDirections();
        }

        //Check if spikehead sees player in all 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            if(directions[i] != null)
            {
                Debug.DrawRay(transform.position, directions[i], Color.red);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

                if (hit.collider != null && !attacking)
                {
                    attacking = true;
                    destination = directions[i];
                    checkTimer = 0;
                    StartCoroutine(StopSpikehead());
                }
            }
        }
    }

    private void Stop()
    {
        destination = transform.position; //Set destination as current position so it doesn't move
        attacking = false;
    }

    private void SetFourDirections()
    {
        directions[0] = transform.right * range; //Right direction
        directions[1] = -transform.right * range; //Left direction
        directions[2] = transform.up * range; //Up direction
        directions[3] = -transform.up * range; //Down direction
    }

    private void SetUPDown()
    {
        directions[0] = transform.up * range; //Up direction
        directions[1] = -transform.up * range; //Down direction
    }

    private void SetLeftRight()
    {
        directions[0] = transform.right * range; //Right direction
        directions[1] = -transform.right * range; //Left direction
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.CompareTag("Player"))
        {
            coll.GetComponent<PlayerLife>().TakeDamage(20);
        }
        else
        {
            Stop(); //Stop spikehead once he hits something
        }
    }

    private IEnumerator StopSpikehead() 
    {
        yield return new WaitForSecondsRealtime(2.5f);
        Stop();
    }
}
