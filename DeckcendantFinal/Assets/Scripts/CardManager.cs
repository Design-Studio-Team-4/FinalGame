using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardManager : MonoBehaviour
{
    public static CardManager cManagerInstance;

    public List<Card> drawpile = new List<Card>();
    public List<Card> deck = new List<Card>();
    public List<Card> discard = new List<Card>();

    public Card[] hand = new Card[5];
    public GameObject handObject;
    public GameObject[] handSlots = new GameObject[5];

    public Card selectedCard;

    public Sprite[] cardFronts;
    public Sprite[] cardBacks;

    private Coroutine playC;

    void Awake()
    {
        if (cManagerInstance == null) { cManagerInstance = this; }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log(hand[i].name);
                Debug.Log(hand[i].cost);
            }
        }
    }

    void Start()
    {
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 damage"));
        deck.Add(new Card("Block", 1, 1, 1, 7, false, false, cardFronts[1], cardBacks[1], "Blocks 7 damage"));
        deck.Add(new Card("Block", 1, 1, 1, 7, false, false, cardFronts[1], cardBacks[1], "Blocks 7 damage"));
        deck.Add(new Card("Block", 1, 1, 1, 7, false, false, cardFronts[1], cardBacks[1], "Blocks 7 damage"));
        deck.Add(new Card("Heal", 2, 1, 1, 5, false, false, cardFronts[2], cardBacks[2], "Heals 5 health"));
        deck.Add(new Card("Heal", 2, 1, 1, 5, false, false, cardFronts[2], cardBacks[2], "Heals 5 health"));

        for (int i = 0; i < 5; i++)
        {
            hand[i] = new Card("Blank", 0, 0, 0, 0, true, false, null, null, "Blank");
        }

        discard = new List<Card>(deck);

        discard = Shuffle(discard);
        Draw();
        discard.Clear();
        handObject.GetComponent<Animator>().Play("HandUp");

        for (int i = 0; i < 5; i++)
        {
            hand[i].used = false;
        }
    }

    public void PlayCard(int cardSlot, int enemySlot)
    {
        playC = StartCoroutine(CardUse(cardSlot, enemySlot));
    }

    private IEnumerator CardUse(int cardSlot, int enemySlot)
    {
        hand[cardSlot].used = true;
        FlipCard(cardSlot);

        yield return new WaitForSeconds(0.50f);

        handSlots[cardSlot].GetComponent<CardScript>().PutBack();

        yield return new WaitForSeconds(0.50f);

        GameObject enemy = BattleManager.bManagerInstance.enemies[enemySlot];

        if (hand[cardSlot].cardType == 0)
        {
            BattleManager.bManagerInstance.player.GetComponent<Animator>().Play("Player_Attack");

            yield return new WaitForSeconds(0.50f);

            enemy.transform.GetChild(0).GetComponent<Animator>().Play("OnHit");

            if (BattleManager.bManagerInstance.cower)
            {
                //...
            }
            else if (enemy.transform.GetChild(0).GetComponent<EnemyScript>().enemy.currentBlockVal > 0)
            {
                if((enemy.transform.GetChild(0).GetComponent<EnemyScript>().enemy.currentBlockVal - hand[cardSlot].power) >= 0)
                {
                   enemy.transform.GetChild(0).GetComponent<EnemyScript>().enemy.currentBlockVal -= hand[cardSlot].power;
                }
                else
                {
                    enemy.transform.GetChild(0).GetComponent<EnemyScript>().enemy.health -= (hand[cardSlot].power - enemy.transform.GetChild(0).GetComponent<EnemyScript>().enemy.currentBlockVal);
                    enemy.transform.GetChild(0).GetComponent<EnemyScript>().enemy.currentBlockVal = 0;
                }
            }
            else
            {
                enemy.transform.GetChild(0).GetComponent<EnemyScript>().enemy.health -= hand[cardSlot].power;
            }

            if(hand[cardSlot].name == "Life Drink" && BattleManager.bManagerInstance.cower == false)
            {
                BattleManager.bManagerInstance.playerHealth += 8;
                BattleManager.bManagerInstance.player.transform.GetChild(2).GetComponent<Animator>().Play("Heal");
            }
        }

        else if (hand[cardSlot].cardType == 3)
        {
            BattleManager.bManagerInstance.player.transform.GetChild(2).GetComponent<Animator>().Play("Effect");

            yield return new WaitForSeconds(0.50f);

            enemy.transform.GetChild(0).GetComponent<Animator>().Play("OnHit");

            enemy.transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown += hand[cardSlot].power;
        }

        int costBeforeSort = hand[cardSlot].cost;

        SortHand();

        yield return new WaitForSeconds(0.20f);

        BattleManager.bManagerInstance.ReduceEnemyCooldown(costBeforeSort);

        StopCoroutine(playC);
    }

    public void Draw()
    {
        SortHand();

        int drawCount = CheckHand();

        if (drawCount < drawpile.Count)
        {
            Drawing(drawCount);
        }

        else if (drawCount >= drawpile.Count)
        {

            if (drawpile.Count != 0)
            {
                drawCount -= drawpile.Count;
                SortHand();
                Drawing(drawpile.Count);
            }

            drawpile.Clear();

            drawpile = new List<Card>(discard);

            discard.Clear();

            if (drawCount != 0)
            {
                drawpile = Shuffle(drawpile);

                SortHand();
                Drawing(drawCount);
            }
        }
        SortHand();
    }

    public void Drawing(int cardsToDraw)
    {
        for (int i = 0; i < (cardsToDraw); i++)
        {
            hand[i].used = false;
            discard.Add(hand[i]);

            hand[i] = drawpile[drawpile.Count - 1];

            drawpile.RemoveAt(drawpile.Count - 1);

            handSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = hand[i].front;
        }   
    }

    public int CheckHand()
    {
        int draw = 0;

        for (int i = 0; i < 5; i++)
        {
            if (hand[i].used)
            {
                draw++;
            }
        }
        return draw;
    }

    public void SortHand()
    {
        for (int j = 0; j <= hand.Length - 2; j++)
        {
            for (int i = 0; i <= hand.Length - 2; i++)
            {
                if (hand[i].used == false && hand[i+1].used)
                {
                    Card tempCard = hand[i + 1];
                    Sprite tempSprite = handSlots[i + 1].transform.GetChild(0).GetComponent<Image>().sprite;

                    hand[i + 1] = hand[i];
                    handSlots[i + 1].transform.GetChild(0).GetComponent<Image>().sprite = handSlots[i].transform.GetChild(0).GetComponent<Image>().sprite;

                    hand[i] = tempCard;
                    handSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = tempSprite;
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (hand[i].used == false)
            {

                handSlots[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = hand[i].front;

                if (hand[i].longName)
                {
                    handSlots[i].transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<TMP_Text>().fontSize = 5.75f;
                }

                else
                {
                    handSlots[i].transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<TMP_Text>().fontSize = 7.75f;
                }

                handSlots[i].transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = hand[i].desc;
                handSlots[i].transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = hand[i].cost.ToString();
                handSlots[i].transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = hand[i].name;
            }
            else
            {
                FlipCard(i);
            }
        }
    }

    public static System.Random rand = new System.Random();

    public List<Card> Shuffle(List<Card> pile)
    {
        for (int i = 0; i < pile.Count; i++)
        {
            Card temp = pile[i];
            int random = i + rand.Next(pile.Count - i);
            pile[i] = pile[random];
            pile[random] = temp;
        }

        return pile;
    }

    public void FlipCard(int index)
    {
        GameObject card = handSlots[index].transform.GetChild(0).gameObject;

        card.GetComponent<Image>().sprite = hand[index].back;
        card.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        card.transform.GetChild(1).GetComponent<TMP_Text>().text = "";
        card.transform.GetChild(2).GetComponent<TMP_Text>().text = "";
    }

    public void NextWave()
    {
        drawpile.Clear();

        for (int i = 0; i < 5; i++)
        {
            hand[i] = new Card("Blank", 0, 0, 0, 0, true, false, null, null, "Blank");
        }

        discard.Clear();

        discard = new List<Card>(deck);

        discard = Shuffle(discard);

        Draw();
        discard.Clear();
        handObject.GetComponent<Animator>().Play("HandUp");

        for (int i = 0; i < 5; i++)
        {
            hand[i].used = false;
            handSlots[i].GetComponent<CardScript>().isMoving = false;
            handSlots[i].GetComponent<Animator>().enabled = true;
        }

        BattleManager.bManagerInstance.enemyTurn = false;
        Hand.handInstance.targeting = false;

        BattleManager.bManagerInstance.waveTransition.SetActive(false);
        BattleManager.bManagerInstance.shop.SetActive(false);
    }

    public struct Card
    {
        public Sprite front;
        public Sprite back;

        public int cardType;
        public int functionType;
        public int power;

        public int cost;
        public string name;
        public string desc;

        public bool used;
        public bool longName;
        
        public Card(string n, int ct, int ft, int c, int p, bool u, bool ln, Sprite f, Sprite b, string d)
        {
            name = n;
            cardType = ct;
            functionType = ft;
            cost = c;
            power = p;
            used = u;
            longName = ln;
            front = f;
            back = b;
            desc = d;
        }
    }
}
