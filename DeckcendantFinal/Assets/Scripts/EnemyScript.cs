using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static BattleManager;

public class EnemyScript : MonoBehaviour
{
    public static EnemyScript enemyInstance;

    public Enemy enemy;

    public SpriteRenderer enemySprite;

    private Color hoverColor;
    private Color normalColor;

    void Awake()
    {
        if (enemyInstance == null) { enemyInstance = this; }
    }

    void Start()
    {
        hoverColor = new Color(1.00f, 0.90f, 0.00f);
        normalColor = new Color(1.00f, 1.00f, 1.00f);
    }

    public void OnMouseOver()
    {
        if (Hand.handInstance.targeting)
        {
            enemySprite.color = hoverColor;
        }
    }

    public void OnMouseExit()
    {
        if (Hand.handInstance.targeting)
        {
            enemySprite.color = normalColor;
        }
    }

    public void OnMouseDown()
    {
        if (Hand.handInstance.targeting)
        {
            Hand.handInstance.targeting = false;
            enemySprite.color = normalColor;

            for (int i = 0; i < 5; i++)
            {
                GameObject slot = CardManager.cManagerInstance.handSlots[i];
                if (slot.GetComponent<CardScript>().selected)
                {
                    CardManager.cManagerInstance.PlayCard(i,enemy.slot);
                    break;
                }
            }
        }
    }
}
