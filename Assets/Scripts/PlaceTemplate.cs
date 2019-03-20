using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTemplate : MonoBehaviour
{
    [SerializeField]
    public GameObject finalObject;
    public Grid test;
    public Vector3Int cellPosition;
    PlayerInteraction playTest;
    public GameObject objTest;
    public GameObject player;

    public Vector2 mousePos;

   

    void Update()
    {
        objTest = player.GetComponent<Inventory>().selectedObject;

        if (finalObject == objTest)
        {
            gameObject.SetActive(true);

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 pos = new Vector2((mousePos.x), (mousePos.y));


            cellPosition = test.LocalToCell(pos);
            transform.position = test.GetCellCenterLocal(cellPosition);
        }
        
    }

 
}
