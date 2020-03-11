using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public GameObject cardManager;
    private CardManager cardManagerScript;
    GameObject SlotCardIsIn;
    bool isCardInASlot;
    private int handIndex;
    bool isFocus;
    bool isMoving;
    float waitTime = 2f;
    public GameObject hand;
    public GameObject OldSlot;

    public GameObject block;

    void Start()
    {
        hand = GameObject.FindGameObjectWithTag("PlayerHand");
        isFocus = false;
        handIndex = (GetComponent<Canvas>().sortingOrder) - 1;
        cardManagerScript = cardManager.GetComponent<CardManager>();
    }
    private void Update()
    {
        if (isFocus)
        {

        }
        
    }
    public void MoveToSlot(GameObject Slot)
    {
        if(isMoving == false)
        {
            StartCoroutine(MoveToSlotEnum(Slot));
        }
        
    }
     IEnumerator MoveToSlotEnum(GameObject SlotToEnter)
    {
        Debug.Log("MoveToSlot Started");
       if (SlotCardIsIn)
       {
           OldSlot = SlotCardIsIn;
       }
       PutCardInSlot(SlotToEnter);
       if (SlotToEnter.GetComponent<CardSlot>().isFocusSlot())
        {
            isFocus = true;
        } 
        isMoving = true;
        Vector3 targetPos = SlotToEnter.transform.position;
        //Vector3 relativePos = targetPos - gameObject.transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < waitTime)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        
        isMoving = false;
        //isFocus = true;
        yield return null;
    }
    public void PutCardInSlot(GameObject slotToEnter)
    {
        
                    slotToEnter.GetComponent<CardSlot>().PutCardInSlot(gameObject);
                    isCardInASlot = true;

              // Debug.LogError("Card slot you are trying to enter is not empty");
                
                
       
       
    }
    public void removeRemoveFocus()
    {

        isFocus = false;
    }
    public GameObject GetOldSlot()
    {
        return OldSlot;
    }
     public void OnClick()
    {
        //MoveToSlot(hand.GetComponent<Hand>().FocusSlot);
       
        if(isFocus == false)
        {
            if (hand.GetComponent<Hand>().GetFocus())
            {
              GameObject oldCard = hand.GetComponent<Hand>().GetFocus();
              //  if (hand.GetComponent<Hand>().FindEmptySlot())
               // {
                    if (oldCard.GetComponent<CardScript>().GetOldSlot() == null)
                    {
                        Debug.Log("OldCard get old slot = null");
                        oldCard.GetComponent<CardScript>().MoveToSlot(hand.GetComponent<Hand>().FindEmptySlot());
                        gameObject.GetComponent<CardScript>().MoveToSlot(hand.GetComponent<Hand>().FocusSlot);
                    } else
                    {
                        oldCard.GetComponent<CardScript>().MoveToSlot(oldCard.GetComponent<CardScript>().GetOldSlot());
                    }
                   
               // }
                //else
               // {
                //    Debug.LogError("There Was no empty slot to return the card in focus to");
                //}
                
            }
            else
            {
                Debug.Log("There was no card in the focus slot");
                gameObject.GetComponent<CardScript>().MoveToSlot(hand.GetComponent<Hand>().FocusSlot);
            }
        }
        {
            if (isFocus)
            {
                //temp...later will play here

                MoveToSlot(OldSlot);
            }
        } 
       /*if (cardManagerScript.hand[handIndex].used == false)
        {
            cardManager.GetComponent<CardManager>().PlayCard(handIndex);
        }
    */
   
    }

    /*public void OnHover()
    {
        if (cardManagerScript.hand[handIndex].used == false)
        {
            GetComponent<Canvas>().sortingOrder = 6;
            GetComponent<Animator>().Play("CardHover");
        }
    }

    public void OnHoverExit()
    {
        if (cardManagerScript.hand[handIndex].used == false)
        {
            GetComponent<Canvas>().sortingOrder = handIndex + 1;
            GetComponent<Animator>().Play("CardHoverExit");
        }
    }

   

   */
}
