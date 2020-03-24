using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    private int handIndex;
    public GameObject hand;

    private Vector3 clickedPos;
    Coroutine clicked;

    void Start()
    {
        clickedPos = new Vector3(375.0f, 575.0f, 0.0f);
        handIndex = (GetComponent<Canvas>().sortingOrder) - 1;
    }

    public void OnHover()
    {
        if (CardManager.cManagerInstance.hand[handIndex].used == false && Hand.handInstance.targeting == false)
        {
            GetComponent<Canvas>().sortingOrder = 6;
            GetComponent<Animator>().Play("CardHover");
        }
    }

    public void OnHoverExit()
    {
        if (CardManager.cManagerInstance.hand[handIndex].used == false && Hand.handInstance.targeting == false)
        {
            GetComponent<Canvas>().sortingOrder = handIndex + 1;
            GetComponent<Animator>().Play("CardHoverExit");
        }
    }

    public void OnClick()
    {
        if (CardManager.cManagerInstance.hand[handIndex].used == false && Hand.handInstance.targeting == false)
        {
            Hand.handInstance.targeting = true;
            GetComponent<Animator>().enabled = false;
            clicked = StartCoroutine(MoveFunction());
        }
    }
    
    IEnumerator MoveFunction()
    {
        float timeSinceStarted = 0f;

        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, clickedPos, timeSinceStarted);

            // If the object has arrived, stop the coroutine
            if (transform.position == clickedPos)
            {
                StopCoroutine(clicked);
            }

            // Otherwise, continue next frame
            yield return null;
        }

        
    }
}

