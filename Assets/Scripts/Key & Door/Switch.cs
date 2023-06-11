using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] Door door;

    [SerializeField] bool isOpenSwitch;
    [SerializeField] bool isCloseSwitch;

    float switchSizeY;
    Vector3 switchUpPos;
    Vector3 switchDownPos;
    float switchSpeed = 1f;
    float timeDelay = 0.5f;

    [SerializeField] bool isPressing = false;

    //[SerializeField] bool doorLocked = true;
    //what kind of Item is required
    [SerializeField] InventoryManager.ALLItems requriedItemType;

    // Start is called before the first frame update
    void Awake()
    {
        switchSizeY = transform.localScale.y / 2;
        switchUpPos = transform.position;
        switchDownPos = new Vector3(transform.position.x, transform.position.y - switchSizeY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressing)
        {
            MoveSwitchDown();
        }
        else if (!isPressing)
        {
            MoveSwitchUp();
        }
    }

    void MoveSwitchDown()
    {
        if(transform.position != switchDownPos)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                switchDownPos,
                switchSpeed * Time.deltaTime
            );
        }
    }

    void MoveSwitchUp()
    {
         if(transform.position != switchUpPos)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                switchUpPos,
                switchSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player")) // coll.gameObject.tag == "Player"
        {
            isPressing = !isPressing;

            if(HasRequiredItem(requriedItemType))
            {
                if(isOpenSwitch && !door.isOpen)
                {
                    door.isOpen = !door.isOpen;
                }
                else if(isCloseSwitch && door.isOpen)
                {
                    door.isOpen = !door.isOpen;
                }
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.CompareTag("Player")) // coll.gameObject.tag == "Player"
        {
            StartCoroutine(SwitchUpDelay(timeDelay));
        }
    }

    IEnumerator SwitchUpDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isPressing = false;
    }
    /*
    public void DoorLockedStatus()
    {
        doorLocked = false;
    }
    */

    public bool HasRequiredItem(InventoryManager.ALLItems requiredItem)
    {
        if(InventoryManager.Instance.InventoryItems.Contains(requiredItem))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
