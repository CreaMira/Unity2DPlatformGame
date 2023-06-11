using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] InventoryManager.ALLItems itemType;
    [SerializeField] Switch switchBehaviour;

    private bool isFollwing;
    public float followSpeed;
    public Transform followTarget = null;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(isFollwing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            InventoryManager.Instance.AddItem(itemType);

            //switchBehaviour.DoorLockedStatus(); //unlock the door switch

            if(!isFollwing)
            {
                PlayerController thePlayer = FindObjectOfType<PlayerController>();
                followTarget = thePlayer.keyFollowPoint;
                isFollwing = true;
            }
        }
    }
}
