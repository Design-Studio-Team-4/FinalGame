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
    IEnumerator co;
    public GameObject block;

    void Start()
    {
        hand = GameObject.FindGameObjectWithTag("PlayerHand");
        OriginalPos = gameObject.transform.position;
        HoverPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 50, gameObject.transform.position.z);
        handIndex = (GetComponent<Canvas>().sortingOrder) - 1;
        cardManagerScript = cardManager.GetComponent<CardManager>();
    }
    private void Update()
    {
       
        
        
    }
  
 

    public void OnHover()
    {
        if (cardManagerScript.hand[handIndex].used == false)
        {
            GetComponent<Canvas>().sortingOrder = 6;
            //GetComponent<Animator>().Play("CardHover");
            co = StartCoroutine(cardManager.GetComponent<CardManager>().MoveCard(handIndex,HoverPos));

        }
    }

    public void OnHoverExit()
    {
        if (cardManagerScript.hand[handIndex].used == false)
        {
            GetComponent<Canvas>().sortingOrder = handIndex + 1;
            //GetComponent<Animator>().Play("CardHoverExit");
            StopCoroutine(cardManager.GetComponent<CardManager>().MoveCard(handIndex, HoverPos));
            gameObject.transform.position = HoverPos;
        }
    }

   

  
}
