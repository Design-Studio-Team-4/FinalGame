using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            Debug.Log("YEET");
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

    public void OnClick()
    {
        if (Hand.handInstance.targeting)
        {
            
        }
    }
}
