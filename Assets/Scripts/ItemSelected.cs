using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelected : MonoBehaviour
{
    public GameObject slot;
    public bool selected;



    public void SetSlot(GameObject thisSlot)
    {
        slot = thisSlot;
    }

    public void SetEnables(bool thisEnabled)
    {
        selected = thisEnabled;
    }
 

}
