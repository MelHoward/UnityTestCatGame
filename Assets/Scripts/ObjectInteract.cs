using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour {

	public bool inventory;	//If true, can be stored in inventory
    public bool destroyable;
    public bool tree;
    public bool stackable;
    public GameObject itemNeeded; //Item needed to interact
    public GameObject drop;
    public bool drops;
    public bool talks;
    public int health;
    public string message;
    public int amountHeld = 0;
    public GameObject self;

	public void DoInteraction()
	{
		gameObject.SetActive (false);

        
    }

    public void Talk()
    {
        Debug.Log (message);
    }

	/*public void Place()
	{
			Vector3 pz = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pz.z = 0;
			gameObject.transform.position = pz;
	}*/
}
