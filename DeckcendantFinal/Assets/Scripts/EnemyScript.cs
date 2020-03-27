using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static BattleManager;

public class EnemyScript : MonoBehaviour
{
    public static EnemyScript enemyInstance;

    public Enemy enemy;

    public int baseHealth;

    public SpriteRenderer enemySprite;

    private Color hoverColor;
    private Color normalColor;

    void Awake()
    {
        if (enemyInstance == null) { enemyInstance = this; }
    }

    void Start()
    {
        baseHealth = enemy.health;

        hoverColor = new Color(1.00f, 0.90f, 0.00f);
        normalColor = new Color(1.00f, 1.00f, 1.00f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            transform.GetChild(1).GetComponent<Animator>().Play("Heal");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            transform.GetChild(1).GetComponent<Animator>().Play("Block");
        }

        Transform indicator = transform.GetChild(2).GetChild(0);

        indicator.GetChild(0).GetComponent<TMP_Text>().text = enemy.cooldown.ToString();

        indicator.GetChild(1).GetComponent<Slider>().value = enemy.health;
        indicator.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = enemy.health.ToString() + "/" + baseHealth;
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
