using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    //hold an array for way points
    [SerializeField] private GameObject[] wayPoints;
    private int currWayPointIndex = 0;

    [SerializeField] private float speed = 2f;
    
    private void FixedUpdate()
    {
         //calculate the distance between current way point and current position
        if ( Vector2.Distance( wayPoints[currWayPointIndex].transform.position, transform.position ) < 0.1f )   
        {
            //count each way point
            currWayPointIndex++;
            if (currWayPointIndex >= wayPoints.Length)
            {
                currWayPointIndex = 0;
            }
            
        }

        transform.position = Vector2.MoveTowards(
                                                    transform.position,
                                                    wayPoints[currWayPointIndex].transform.position,
                                                    Time.deltaTime * speed
                                                );
    }
}
