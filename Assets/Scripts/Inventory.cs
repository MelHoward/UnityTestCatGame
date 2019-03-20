using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Iventory2D
{
    public GameObject[] items;
}

public class Inventory : MonoBehaviour {


    public Iventory2D[] inventory = new Iventory2D[10];
    public Button[] inventoryButtons = new Button[10];
    public ObjectInteract currentObject = null;
    public GameObject selectedObject;
    public bool[] selected = new bool[10];
    public int itemNum, prevNum, findNum;

    void Start()
    {
        itemNum = 0;
        prevNum = 0;
        for (int i = 0; i < 10; i++)
        {
            selected[i] = false;
        }
        currentObject = GetComponent<PlayerInteraction>().GetCurrentInterObject();
        
        
    }

    void Update()
    {
        ItemSelector();
        currentObject = GetComponent<PlayerInteraction>().GetCurrentInterObject();
        selectedObject = inventory[itemNum].items[0];
    }


    public void AddItem(GameObject item)
    {
        bool itemAdded = false;

        //Find first open slot
        for (int i = 0; i < inventory.Length; i++)
        {

            if (inventory[i].items[0] != null)
            {
                if (currentObject.stackable)
                {
                    for (int j = 0; j < inventory.Length; j++)
                    {
                        if (inventory[i].items[j] == null)
                        {
                            selected[i] = true;
                            inventory[i].items[j] = item;
                            break;
                        }
                    }
                }
            }
            else if (inventory[i].items[0] == null)
                {

                    inventory[i].items[0] = item;

                    Debug.Log(item.name + " was added");
                    itemAdded = true;


                    inventoryButtons[i].image.overrideSprite = item.GetComponent<SpriteRenderer>().sprite;


                    item.SendMessage("DoInteraction");
                    break;
                }
            }

            //InvFull
            if (!itemAdded)
            {
                Debug.Log("Inventory Full!");
            }

        }
    

    public bool FindItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].items[0] == item)
            {
                findNum = 30;
                return true;
            }
        }
        return false;
    }

    public void ItemSelector()
    { 
        prevNum = itemNum;
        
        if (Input.GetAxis ("Mouse ScrollWheel") > 0f)
        {
            itemNum++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            itemNum--;
        }
        if (itemNum > 9)
        {
            itemNum = 9;
        }
        if (itemNum < 0)
        {
            itemNum = 0;
        }

        if (prevNum != itemNum)
        {
            inventoryButtons[prevNum].GetComponent<Outline>().enabled = false;
            selected[prevNum] = false;
        }

        inventoryButtons[itemNum].GetComponent<Outline>().enabled = true;

        selected[itemNum] = true;
        
    }

    public void StackItems(GameObject item)
    {
        if (currentObject.stackable && FindItem(currentObject.self))
            {
                
            }
    }

    public GameObject GetSelectedItem()
    {
        return selectedObject;
    }

}

