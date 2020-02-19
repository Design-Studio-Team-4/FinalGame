using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    protected int idle;

    public void OnHover()
    {
        idle = GetComponent<Canvas>().sortingOrder;
        GetComponent<Canvas>().sortingOrder = 6;
        GetComponent<Animator>().Play("CardHover");
    }

    public void OnHoverExit()
    {
        GetComponent<Canvas>().sortingOrder = idle;
        GetComponent<Animator>().Play("CardHoverExit");
    }
}
