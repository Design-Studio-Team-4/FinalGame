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
        Transform indicator = transform.GetChild(2).GetChild(0);

        if (enemy.health > baseHealth)
        {
            enemy.health = baseHealth;
            indicator.GetChild(1).GetComponent<Slider>().value = enemy.health;
            indicator.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = enemy.health.ToString() + "/" + baseHealth;
        }
        else if (enemy.health <= 0)
        {
            enemy.cooldown = -2;
            bManagerInstance.enemies[enemy.slot] = null;

            bManagerInstance.CheckEnemyTurn();

            CoinandScore.coinsAndScoreInstance.coins += Random.Range(1, 11);
            CoinandScore.coinsAndScoreInstance.score = CoinandScore.coinsAndScoreInstance.coins;

            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            indicator.GetChild(1).GetComponent<Slider>().value = enemy.health;
            indicator.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = enemy.health.ToString() + "/" + baseHealth;
        }

        if (enemy.currentBlockVal > 0)
        {
            indicator.GetChild(2).gameObject.SetActive(true);
            indicator.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = enemy.currentBlockVal.ToString();
        }
        else if (enemy.currentBlockVal <= 0)
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
