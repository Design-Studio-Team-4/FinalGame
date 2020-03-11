using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardManager;
    private CardManager cardManagerScript;

    void Start()
    {
        cardManagerScript = cardManager.GetComponent<CardManager>();
    }

    public void Draw()
    {
        cardManagerScript.Draw();
    }
}
