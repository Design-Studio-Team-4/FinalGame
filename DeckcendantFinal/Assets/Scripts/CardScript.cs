using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    private int idle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void OnClick()
    {
        // 
    }
}
