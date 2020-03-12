using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public GameObject cardManager;
    private CardManager cardManagerScript;
   
    private int handIndex;
   
    public GameObject hand;
    Vector3 OriginalPos;
    Vector3 HoverPos;
    bool isFocus;
    IEnumerator MoveCard;

    public GameObject block;
    private IEnumerator moveToFocus;
    private IEnumerator moveToHand;
    private IEnumerator onHover;
    private IEnumerator onHoverExit;
    public bool isMoving; 
    void Start()
    {
        isMoving = false;
        hand = GameObject.FindGameObjectWithTag("PlayerHand");
        OriginalPos = gameObject.transform.position;
        HoverPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 50, gameObject.transform.position.z);
        handIndex = (GetComponent<Canvas>().sortingOrder) - 1;
        cardManagerScript = cardManager.GetComponent<CardManager>();
        
    }
    private void Update()
    {
       
        
        
  
 
        handIndex = (GetComponent<Canvas>().sortingOrder) - 1;
        cardManagerScript = cardManager.GetComponent<CardManager>();
    }
    public void setIsMoving(bool val)
    {
        isMoving = val;
    }
    public bool getIsFocus()
    {
        return isFocus;
    }
    public void setIsFocus(bool val)
    {
        isFocus = val;
    }
    public void OnHover()
    {
        if (cardManagerScript.hand[handIndex].used == false)
        {
            GetComponent<Canvas>().sortingOrder = 6;
            //GetComponent<Animator>().Play("CardHover");
            onHover = cardManager.GetComponent<CardManager>().MoveCard(handIndex,HoverPos);
            StartCoroutine(onHover);
        }
    }

    public void OnHoverExit()
    { 
        //StopCoroutine(onHover);
        if (cardManagerScript.hand[handIndex-1].used == false)
        {
            GetComponent<Canvas>().sortingOrder = handIndex + 1;
            //GetComponent<Animator>().Play("CardHoverExit");
           
            onHoverExit = cardManager.GetComponent<CardManager>().MoveCard(handIndex, OriginalPos);
            StartCoroutine(onHoverExit);
            gameObject.transform.position = OriginalPos;
        }
    }

    public void OnClick()
    {
        if (cardManagerScript.hand[handIndex].used == false)
        {
            if (isMoving == false)
                {
            //cardManager.GetComponent<CardManager>().PlayCard(handIndex);
                if (isFocus)
                {
                
                    cardManagerScript.PlayCard(handIndex);
                    moveToHand = cardManager.GetComponent<CardManager>().MoveCard(handIndex, OriginalPos);
                    StartCoroutine(moveToHand);
                    isFocus = false;
                }
                else
                {   
                 cardManagerScript.BringToFocus(this.gameObject);
                }
            }
            
        }
    }
}
