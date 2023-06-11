using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<ALLItems> InventoryItems = new List<ALLItems>(); 

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem( ALLItems item)
    {
        //if we do not have the item
        if( !InventoryItems.Contains(item) )
        {
            //add the item
            InventoryItems.Add(item);
        }
    }

    public void RemoveItem( ALLItems item)
    {
        //if we do have the item
        if( InventoryItems.Contains(item) )
        {
            //remove the item
            InventoryItems.Remove(item);
        }
    }

    public enum ALLItems  //create a list of all the item in list
    {
        KeyRed,
        KeyBlue
    }
}
