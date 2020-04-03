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
    public Color normalColor;

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
        Transform indicator = transform.GetChild(2).GetChild(0);

        if (enemy.health > baseHealth)
        {
            enemy.health = baseHealth;
            indicator.GetChild(1).GetComponent<Slider>().value = enemy.health;
            indicator.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = enemy.health.ToString() + "/" + baseHealth;
        }
        else if (enemy.health <= 0)
        {
            Debug.Log(bManagerInstance.enemies[enemy.slot]);
            bManagerInstance.enemies[enemy.slot] = null;
            Debug.Log(bManagerInstance.enemies[enemy.slot]);

            // bManagerInstance.CheckEnemyTurn();

            CoinandScore.coinsAndScoreInstance.coins += 2;
            CoinandScore.coinsAndScoreInstance.score += 2;

            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            indicator.GetChild(1).GetComponent<Slider>().value = enemy.health;
            indicator.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = enemy.health.ToString() + "/" + baseHealth;
        }

        if (enemy.currentBlockVal > 20)
        {
            indicator.GetChild(2).gameObject.SetActive(true);
            enemy.currentBlockVal = 20;
            indicator.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = enemy.currentBlockVal.ToString();
        }
        else if (enemy.currentBlockVal > 0)
        {
            indicator.GetChild(2).gameObject.SetActive(true);
            indicator.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = enemy.currentBlockVal.ToString();
        }
        else
        {
            indicator.GetChild(2).gameObject.SetActive(false);
            enemy.currentBlockVal = 0;
            indicator.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = enemy.currentBlockVal.ToString();
        }

        if(enemy.cooldown == -1)
        {
            indicator.GetChild(0).GetComponent<TMP_Text>().text = "-";
        }
        else
        {
            indicator.GetChild(0).GetComponent<TMP_Text>().text = enemy.cooldown.ToString();
        }
    }

    public void OnMouseOver()
    {
        bool isMoving = false;

        for (int i = 0; i < 5; i++)
        {
            GameObject moving = CardManager.cManagerInstance.handSlots[i];
            if (moving.GetComponent<CardScript>().selected)
            {
                isMoving = moving.GetComponent<CardScript>().isMoving;
                break;
            }
        }

        if (Hand.handInstance.targeting && !isMoving)
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
        bool isMoving = false;

        for (int i = 0; i < 5; i++)
        {
            GameObject moving = CardManager.cManagerInstance.handSlots[i];
            if (moving.GetComponent<CardScript>().selected)
            {
                isMoving = moving.GetComponent<CardScript>().isMoving;
                break;
            }
        }

        if (Hand.handInstance.targeting && !isMoving)
        {
            enemySprite.color = normalColor;

            for (int i = 0; i < 5; i++)
            {
                GameObject slot = CardManager.cManagerInstance.handSlots[i];
                if (slot.GetComponent<CardScript>().selected)
                {
                    if(CardManager.cManagerInstance.hand[i].cardType == 3 && enemy.cooldown == -1)
                    {
                        // ...
                    }
                    else
                    {
                        slot.transform.GetChild(0).GetComponent<Image>().color = CardScript.cScriptInstance.grey;
                        slot.transform.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = CardScript.cScriptInstance.grey;
                        slot.transform.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().color = CardScript.cScriptInstance.grey;

                        CardManager.cManagerInstance.PlayCard(i, enemy.slot);
                        break;
                    }
                }
            }
        }
    }
}
