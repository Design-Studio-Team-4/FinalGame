using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    private int handIndex;
    public GameObject hand;

    void Start()
    {
        handIndex = (GetComponent<Canvas>().sortingOrder) - 1;
    }

    public void OnHover()
    {
        if (CardManager.cManagerInstance.hand[handIndex].used == false)
        {
            GetComponent<Canvas>().sortingOrder = 6;
            GetComponent<Animator>().Play("CardHover");
        }
    }

    public void OnHoverExit()
    {
        if (CardManager.cManagerInstance.hand[handIndex].used == false)
        {
            GetComponent<Canvas>().sortingOrder = handIndex + 1;
            GetComponent<Animator>().Play("CardHoverExit");
        }
    }

    public void OnClick()
    {
        if (CardManager.cManagerInstance.hand[handIndex].used == false)
        {
            
            
            // CardManager.cManagerInstance.PlayCard(handIndex);
        }
    }
    /* IEnumerator MoveFunction()
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.DeltaTime;
            obj.transform.position = Vector3.Lerp(obj.transform.position, newPosition, timeSinceStarted);

            // If the object has arrived, stop the coroutine
            if (obj.transform.position == newPosition)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    } */
}

