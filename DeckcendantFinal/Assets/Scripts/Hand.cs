using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardManager;
    private CardManager cardManagerScript;
    public GameObject FocusSlot;
    GameObject[] Slots = new GameObject[4];
    void Start()
    {
        cardManagerScript = cardManager.GetComponent<CardManager>();
    }

    public void Draw()
    {
        cardManagerScript.Draw();
    }
    
    public GameObject GetFocus()
    {
        return FocusSlot.GetComponent<CardSlot>().GetCardInSlot();
    }
    

    public GameObject FindEmptySlot()
    {
        bool foundEmpty = false;
        for (int i = 0; i < Slots.Length; i++)
        {
            //getstate returns TRUE if EMPTY
            if (Slots[i].GetComponent<CardSlot>().getState())
            {
               // foundEmpty = true;
                return Slots[i];

            }
        }
        return null;
    }
}
