using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvents : MonoBehaviour
{
    public SpriteRenderer enemy;

    private Color hoverColor;
    private Color normalColor;

    void Start()
    {
        hoverColor = new Color(1.00f, 0.90f, 0.00f);
        normalColor = new Color(1.00f, 1.00f, 1.00f);
    }

    public void OnMouseOver()
    {
        if (Hand.handInstance.targeting)
        {
            Debug.Log("YEET");
            enemy.color = hoverColor;
        }
    }

    public void OnMouseExit()
    {
        if (Hand.handInstance.targeting)
        {
            enemy.color = normalColor;
        }
    }

    public void OnClick()
    {
        if (Hand.handInstance.targeting)
        {
            
        }
    }
}
