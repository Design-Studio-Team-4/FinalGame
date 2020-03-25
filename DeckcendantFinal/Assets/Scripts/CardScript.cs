using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardScript : MonoBehaviour
{
    private int handIndex;
    public GameObject hand;

    private Vector3 clickedPos;
    private Vector3 originalPos;
    private bool isMoving;
    private bool selected;

    private Color grey;
    private Color normal;

    Coroutine click;

    void Start()
    {
        clickedPos = new Vector3(375.0f, 525.0f, 0.0f);
        originalPos = transform.position;

        isMoving = false;
        selected = false;

        grey = new Color(0.47f, 0.47f, 0.47f);
        normal = new Color(1.00f, 1.00f, 1.00f);

        handIndex = (GetComponent<Canvas>().sortingOrder) - 1;
    }

    public void OnHover()
    {
        if (!CardManager.cManagerInstance.hand[handIndex].used && !Hand.handInstance.targeting && !isMoving)
        {
            GetComponent<Canvas>().sortingOrder = 6;
            GetComponent<Animator>().Play("CardHover");
        }
    }

    public void OnHoverExit()
    {
        if (!CardManager.cManagerInstance.hand[handIndex].used && !Hand.handInstance.targeting && !isMoving)
        {
            GetComponent<Canvas>().sortingOrder = handIndex + 1;
            GetComponent<Animator>().Play("CardHoverExit");
        }
    }

    public void OnClick()
    {
        if (!CardManager.cManagerInstance.hand[handIndex].used && !Hand.handInstance.targeting && !isMoving)
        {
            CardManager.cManagerInstance.selectedCard = CardManager.cManagerInstance.hand[handIndex];
            Hand.handInstance.targeting = true;

            selected = true;

            for (int i = 0; i < 5; i++)
            {
                GameObject slot = CardManager.cManagerInstance.handSlots[i];
                if (!slot.GetComponent<CardScript>().selected)
                {
                    slot.transform.GetChild(0).GetComponent<Image>().color = grey;
                    slot.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = grey;
                    slot.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().color = grey;
                }
            }

            isMoving = true;
            GetComponent<Animator>().enabled = false;
            click = StartCoroutine(Select(clickedPos));
        }

        else if (Hand.handInstance.targeting && !isMoving && selected)
        {
            CardManager.cManagerInstance.selectedCard = CardManager.cManagerInstance.hand[handIndex];
            Hand.handInstance.targeting = false;

            for (int i = 0; i < 5; i++)
            {
                GameObject slot = CardManager.cManagerInstance.handSlots[i];
                if (!slot.GetComponent<CardScript>().selected)
                {
                    slot.transform.GetChild(0).GetComponent<Image>().color = normal;
                    slot.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = normal;
                    slot.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().color = normal;
                }
            }

            selected = false;
            GetComponent<Canvas>().sortingOrder = handIndex + 1;

            isMoving = true;
            click = StartCoroutine(Deselect(originalPos));
        }
    }
    
    IEnumerator Select(Vector3 end)
    {
        float timeSinceStarted = 0f;

        while (isMoving)
        {
            timeSinceStarted += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, end, timeSinceStarted);
            float diff = Mathf.Abs(transform.position.sqrMagnitude - end.sqrMagnitude);

            // If the object has arrived, stop the coroutine
            if (diff < 0.1)
            {
                isMoving = false;

                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
            
        }
        StopCoroutine(click);
    }

    IEnumerator Deselect(Vector3 end)
    {
        float timeSinceStarted = 0f;

        while (isMoving)
        {
            timeSinceStarted += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, end, timeSinceStarted);
            float diff = Mathf.Abs(transform.position.sqrMagnitude - end.sqrMagnitude);

            // If the object has arrived, stop the coroutine
            if (diff < 0.1)
            {
                isMoving = false;

                GetComponent<Animator>().enabled = true;
                GetComponent<Animator>().Play("CardReturn");

                yield break;
            }

            // Otherwise, continue next frame
            yield return null;

        }
        StopCoroutine(click);
    }
}