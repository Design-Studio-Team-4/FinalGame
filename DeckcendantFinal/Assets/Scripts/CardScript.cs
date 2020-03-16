using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public GameObject cardManager;
    private CardManager cardManagerScript;

    private int handIndex;
    public GameObject hand;

    private Vector3 start;
    private Vector3 end;
    public Coroutine inst = null;

    void Start()
    {
        handIndex = (GetComponent<Canvas>().sortingOrder) - 1;
        cardManagerScript = cardManager.GetComponent<CardManager>();

        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        end = new Vector3(transform.position.x, 30f, transform.position.z);
    }

    public void OnHover()
    {
        if (cardManagerScript.hand[handIndex].used == false)
        {
            GetComponent<Canvas>().sortingOrder = 6;

            Debug.Log("YEET");

            inst = StartCoroutine(MoveFromTo());

            //GetComponent<Animator>().Play("CardHover");
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

    public void OnClick()
    {
        if (cardManagerScript.hand[handIndex].used == false)
        {
            cardManagerScript.PlayCard(handIndex);
        }
    }

    private IEnumerator MoveFromTo()
    {
        float step = (0.1f / (start - end).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            if(transform.position == end)
            {
                StopCoroutine(inst);
            }

            else
            {
                t += step; // Goes from 0 to 1, incrementing by step each time
                transform.position = Vector3.Lerp(start, end, t); // Move objectToMove closer to b
                yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
            }
        }
        
    }

}

