using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
