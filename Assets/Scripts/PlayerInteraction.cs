using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	public GameObject currentInterObj = null;
	public ObjectInteract currentInterObjScript = null;
	public Inventory inventory;
    

	void Update()
	{
		
		if (Input.GetButtonDown ("Interact") && currentInterObj) 
		{
			//Check to see if this can be stored in inventory
			if (currentInterObjScript.inventory) 
			{
				inventory.AddItem (currentInterObj);
                currentInterObj = null;
			}
            if (currentInterObjScript.destroyable)
            {
                if (currentInterObjScript.tree)
                {
                    if (HoldingCorrectItem())
                        {
                        Debug.Log(currentInterObj.name + " hit");
                        currentInterObjScript.health--;
                            if (currentInterObjScript.health == 0)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    Instantiate(currentInterObjScript.drop, currentInterObj.transform.position, Quaternion.identity);
                                }
                                if (currentInterObjScript.tree)
                                {
                                    currentInterObj.SetActive(false);
                                }
                            }
                        }
                    else
                    {
                        Debug.Log(currentInterObj.name + " can't be hit without Axe!");
                    }
                }
            }
            if (currentInterObjScript.talks)
            {
                currentInterObjScript.Talk(); 
            }
		}
        /*if (Input.GetButtonDown("Fire1"))
			{
				currentInterObj.SendMessage ("Place");
			}*/

        if (currentInterObj != null && currentInterObjScript.drops == true)
        {
            MoveToChracter();
        }
        



    }

    public ObjectInteract GetCurrentInterObject()
    {
        return currentInterObjScript;
    }
		
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("interObject"))
		{
			Debug.Log (other.name);
			currentInterObj = other.gameObject;
			currentInterObjScript = currentInterObj.GetComponent <ObjectInteract> ();
            

        }
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag ("interObject")) {
			if (other.gameObject == currentInterObj) {

                currentInterObj = null;
			}
		}
	}

    void MoveToChracter()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        
       
        Vector2 targetPosition = Player.transform.position;

        Vector2 currentPos = currentInterObj.transform.position;

        if (Vector2.Distance(currentPos, targetPosition) > .1f)
        {
            Vector3 directionOfTravel = targetPosition - currentPos;
            //now normalize the direction, since we only want the direction information
            directionOfTravel.Normalize();
            //scale the movement on each axis by the directionOfTravel vector components

            currentInterObj.transform.Translate(
                (directionOfTravel.x * 3.5f * Time.deltaTime),
                (directionOfTravel.y * 3.5f * Time.deltaTime),
                (directionOfTravel.z * 3.5f * Time.deltaTime),
                Space.World);
        }

        if (Vector2.Distance(currentPos, targetPosition) < .1f)
        {
            
                inventory.AddItem(currentInterObj);
                currentInterObj = null;
            
        }

    }

    bool HoldingCorrectItem()
    {
        return inventory.inventory[inventory.GetComponent<Inventory>().itemNum].items[0] == currentInterObjScript.itemNeeded && inventory.selected[inventory.GetComponent<Inventory>().itemNum] == true;
    }



    
}
	
