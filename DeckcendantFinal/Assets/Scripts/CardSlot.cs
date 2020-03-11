using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    bool isEmpty = true;
    public GameObject CardInSlot;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RemoveCardInSlot()
    {
        if (CardInSlot)
        {
            // CardInSlot.GetComponent<CardScript>().removeCardFromSlot();
            CardInSlot = null;
            isEmpty = true;
        }
        
    }
    public void PutCardInSlot(GameObject NewCardInSlot)
    {
        if (NewCardInSlot)
        {
            if (isEmpty)
            {
                CardInSlot = NewCardInSlot;
                isEmpty = false;
            }
            else
            {
                Debug.LogError("Slot is not empty");
            }
        }
        else
        {
            Debug.LogError("You are not Passing a card to the slot");
        }
    }
    public void changeState(GameObject NewCard)
    {
        if (isEmpty)
        {
            if (NewCard)
            {
                PutCardInSlot(NewCard);
            } 
        }
        else
        {
            Debug.LogError("Using wrong function, if trying to empty slot use changeState() instead.");
        }
    }
    public void changeState()
    {
        if(isEmpty == false)
        {
            RemoveCardInSlot();
        }
        else
        {
            Debug.LogError("Using wrong function, if trying to fill the slot use changeState(GameObject NewCard) instead.");
        }
    }
    public bool getState()
    {
        return isEmpty;
    }
    public GameObject GetCardInSlot()
    {
        return CardInSlot;
    }
    
}
