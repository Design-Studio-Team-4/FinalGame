using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextPage : MonoBehaviour
{
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public int pageNum = 1;
    public void NextPage()
    {
        pageNum++;

        if (pageNum > 3)
        {
            pageNum = 1;
        }
    }
    public void LastPage()
    {
        pageNum--;

        if (pageNum < 1) 
        {
            pageNum = 3;
        }
    }

    public void Update()
    {
        if (pageNum == 1)
        {
            page1.SetActive(true);
            page2.SetActive(false);
            page3.SetActive(false);
        }
        if (pageNum == 2)
        {
            page1.SetActive(false);
            page2.SetActive(true);
            page3.SetActive(false);
        }
        if (pageNum == 3)
        {
            page1.SetActive(false);
            page2.SetActive(false);
            page3.SetActive(true);
        }

    }
}
