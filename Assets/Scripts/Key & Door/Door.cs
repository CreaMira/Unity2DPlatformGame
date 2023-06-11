using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    Vector3 closedPos;
    Vector3 openPos;
    float movingSpeed = 10.0f;

    // Start is called before the first frame update
    void Awake()
    {
        closedPos = transform.position;
        openPos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen)
        {
            OpenDoor();
        }
        else if (!isOpen)
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        if(transform.position != openPos)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPos,
                movingSpeed * Time.deltaTime
            );
        }
    }

    void CloseDoor()
    {
        if(transform.position != closedPos)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                closedPos,
                movingSpeed * Time.deltaTime
            );
        }
    }
    
}
