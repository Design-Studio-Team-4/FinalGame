using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardScript : MonoBehaviour
{
    public static CardScript cScriptInstance;

    private int handIndex;
    public GameObject hand;

    private Vector3 clickedPos;
    private Vector3 originalPos;
    private Vector3 offset;

    public bool isMoving;
    public bool selected;
    public bool disabled;

    public Color grey;
    public Color normal;

    Coroutine click;

    private float resH;
    private float resW;

    void Awake()
    {
        if (cScriptInstance == null) { cScriptInstance = this; }
    }

    void Start()
    {
        resH = Screen.height;
        resW = Screen.width;

        clickedPos = new Vector3(415.0f * (resW/1920), 575.0f * (resH/1080), 0.0f);
        offset = new Vector3(0.0f, 63.0f * (resH / 1080), 0.0f);
        originalPos = transform.position;

        isMoving = false;
        selected = false;
        disabled = false;

        grey = new Color(0.47f, 0.47f, 0.47f);
        normal = new Color(1.00f, 1.00f, 1.00f);

        handIndex = (GetComponent<Canvas>().sortingOrder) - 1;
    }

    void Update()
    {
        int highest = FindHighestCooldown();

        if (!CardManager.cManagerInstance.hand[handIndex].used && CardManager.cManagerInstance.hand[handIndex].cost > highest)
        {
            Debug.Log(highest);
            disabled = true;
        }
        else if (CardManager.cManagerInstance.hand[handIndex].cost <= highest)
        {
            
            disabled = false;
        }

        if (BattleManager.bManagerInstance.enemyTurn == true || disabled == true)
        {
            transform.GetChild(0).GetComponent<Image>().color = grey;
            transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = grey;
            transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().color = grey;

            GetComponent<Animator>().Play("CardReturn");
        }
        else if (Hand.handInstance.targeting == false && BattleManager.bManagerInstance.enemyTurn == false || (disabled == false && Hand.handInstance.targeting == false && BattleManager.bManagerInstance.enemyTurn == false))
        {
            transform.GetChild(0).GetComponent<Image>().color = normal;
            transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = normal;
            transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().color = normal;
        }

        
    }

    public int FindHighestCooldown()
    {
        int highestCooldown = 0;

        for (int i = 0; i < 3; i++)
        {
            if (BattleManager.bManagerInstance.enemies[i] == null)
            {
                continue;
            }

            highestCooldown = BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown;
           
            if (BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown > highestCooldown && BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown != -1)
            {
                highestCooldown = BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown;
            }

        }
        return highestCooldown;
    }

    public void OnHover()
    {
        if (!CardManager.cManagerInstance.hand[handIndex].used && !Hand.handInstance.targeting && !isMoving && !BattleManager.bManagerInstance.enemyTurn && !disabled)
        {
            GetComponent<Canvas>().sortingOrder = 6;
            GetComponent<Animator>().Play("CardHover");
        }
    }

    public void OnHoverExit()
    {
        if (!CardManager.cManagerInstance.hand[handIndex].used && !Hand.handInstance.targeting && !isMoving && !BattleManager.bManagerInstance.enemyTurn && !disabled)
        {
            GetComponent<Canvas>().sortingOrder = handIndex + 1;
            GetComponent<Animator>().Play("CardHoverExit");
        }
    }

    public void OnClick()
    {
        if(!CardManager.cManagerInstance.hand[handIndex].used && !Hand.handInstance.targeting && !isMoving && !BattleManager.bManagerInstance.enemyTurn && (CardManager.cManagerInstance.hand[handIndex].cardType == 1 || CardManager.cManagerInstance.hand[handIndex].cardType == 2) && !disabled)
        {
            GetComponent<Animator>().Play("CardTop");

            CardManager.cManagerInstance.hand[handIndex].used = true;
            CardManager.cManagerInstance.FlipCard(handIndex);

            if(CardManager.cManagerInstance.hand[handIndex].cardType == 1)
            {
                BattleManager.bManagerInstance.player.transform.GetChild(2).GetComponent<Animator>().Play("Block");

                if(CardManager.cManagerInstance.hand[handIndex].name == "Cower")
                {
                    Debug.Log("COWER PLAYED");

                    BattleManager.bManagerInstance.cower = true;
                }

                BattleManager.bManagerInstance.playerCurrentBlockVal += CardManager.cManagerInstance.hand[handIndex].power;
                BattleManager.bManagerInstance.ReduceEnemyCooldown(CardManager.cManagerInstance.hand[handIndex].cost);
            }

            else if (CardManager.cManagerInstance.hand[handIndex].cardType == 2)
            {
                BattleManager.bManagerInstance.player.transform.GetChild(2).GetComponent<Animator>().Play("Heal");

                BattleManager.bManagerInstance.playerHealth += CardManager.cManagerInstance.hand[handIndex].power;
                BattleManager.bManagerInstance.ReduceEnemyCooldown(CardManager.cManagerInstance.hand[handIndex].cost);
            }

            GetComponent<Animator>().Play("CardHoverExit");
            GetComponent<Canvas>().sortingOrder = handIndex + 1;
            CardManager.cManagerInstance.SortHand();
        }

        else if (!CardManager.cManagerInstance.hand[handIndex].used && !Hand.handInstance.targeting && !isMoving && !BattleManager.bManagerInstance.enemyTurn && !disabled)
        {
            GetComponent<Animator>().Play("CardTop");

            CardManager.cManagerInstance.selectedCard = CardManager.cManagerInstance.hand[handIndex];
            Hand.handInstance.targeting = true;

            selected = true;

            for (int i = 0; i < 5; i++)
            {
                GameObject slot = CardManager.cManagerInstance.handSlots[i];
                if (!slot.GetComponent<CardScript>().selected)
                {
                    CardManager.cManagerInstance.handSlots[i].transform.GetChild(0).GetComponent<Image>().color = grey;
                    CardManager.cManagerInstance.handSlots[i].transform.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = grey;
                    CardManager.cManagerInstance.handSlots[i].transform.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().color = grey;
                }
            }

            isMoving = true;
            click = StartCoroutine(Select(clickedPos));
        }

        else if (Hand.handInstance.targeting && !isMoving && selected && !BattleManager.bManagerInstance.enemyTurn)
        {
            PutBack();
        }
    }

    public void PutBack()
    {
        transform.GetChild(0).GetComponent<Image>().color = grey;
        transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = grey;
        transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().color = grey;

        selected = false;
        GetComponent<Canvas>().sortingOrder = handIndex + 1;

        isMoving = true;
        click = StartCoroutine(Deselect(originalPos - offset));
    }
    
    IEnumerator Select(Vector3 end)
    {
        yield return new WaitForSeconds(0.1f);

        GetComponent<Animator>().enabled = false;

        float timeSinceStarted = 0f;

        while (isMoving)
        {
            timeSinceStarted += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, end, timeSinceStarted);
            float diff = Mathf.Abs(transform.position.sqrMagnitude - end.sqrMagnitude);

            // If the object has arrived, stop the coroutine
            if (diff < 0.1)
            {
                isMoving = false;

                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
            
        }
        StopCoroutine(click);
    }

    IEnumerator Deselect(Vector3 end)
    {
        float timeSinceStarted = 0f;

        while (isMoving)
        {
            timeSinceStarted += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, end, timeSinceStarted);
            float diff = Mathf.Abs(transform.position.sqrMagnitude - end.sqrMagnitude);

            // If the object has arrived, stop the coroutine
            if (diff < 0.1)
            {
                yield return new WaitForSeconds(0.55f);

                isMoving = false;
                Hand.handInstance.targeting = false;

                for (int i = 0; i < 3; i++)
                {
                    if (BattleManager.bManagerInstance.enemies[i] == null)
                    {
                        continue;
                    }

                    BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemySprite.color = EnemyScript.enemyInstance.normalColor;
                }

                GetComponent<Animator>().enabled = true;
                GetComponent<Animator>().Play("CardReturn");

                yield break;
            }

            // Otherwise, continue next frame
            yield return null;

        }
        StopCoroutine(click);
    }
}