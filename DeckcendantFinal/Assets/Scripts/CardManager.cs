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
    // public List<Card> discard = new List<Card>();

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

    void Start()
    {
       
        for (int i = 0; i < 5; i++)
        {
            hand[i] = new Card("Blank", 0, 0, 0, 0, true, false, null, null, "Blank");
        }

        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        deck.Add(new Card("Strike", 0, 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        deck.Add(new Card("Block", 1, 1, 1, 10, false, false, cardFronts[1], cardBacks[1], "Blocks 10 damage"));
        deck.Add(new Card("Block", 1, 1, 1, 10, false, false, cardFronts[1], cardBacks[1], "Blocks 10 damage"));
        deck.Add(new Card("Block", 1, 1, 1, 10, false, false, cardFronts[1], cardBacks[1], "Blocks 10 damage"));
        deck.Add(new Card("Heal", 2, 1, 1, 5, false, false, cardFronts[2], cardBacks[2], "Heals 5 health"));
        deck.Add(new Card("Heal", 2, 1, 1, 5, false, false, cardFronts[2], cardBacks[2], "Heals 5 health"));

        // deck.Add(new Card("First Strike", 1, 2, 7, false, true, cardFronts[3], cardBacks[0], "Deals 7 damage. If targeted enemies counter is less than 3, gain 3 block"));
        // deck.Add(new Card("Hail of Daggers", 1, 4, 4, false, true, cardFronts[4], cardBacks[0], "Deal 4 damage to each enemy"));
        // deck.Add(new Card("Shield Ward", 4, 3, 0, false, true, cardFronts[5], cardBacks[3], "Disregard all enemy Block on your next attack"));
        // deck.Add(new Card("Soulfire Sacrifice", 1, 5, 0, false, true, cardFronts[6], cardBacks[0], "Deal 50 damage to yourself to execute an enemy"));
        // deck.Add(new Card("Stasis", 4, 4, 0, false, false, cardFronts[7], cardBacks[3], "Non-boss enemies will not attack this round."));
        // deck.Add(new Card("Time Steal", 4, 2, 4, false, true, cardFronts[8], cardBacks[3], "Increase the cooldown of enemy attack by 4"));
        // deck.Add(new Card("Turn the Tides", 1, 30, 0, false, true, cardFronts[9], cardBacks[0], "Deal 30 damage to each enemy. Can only be played if you have less than 10HP"));
        // deck.Add(new Card("Life Drink", 1, 2, 8, false, true, cardFronts[10], cardBacks[0], "Increase the cooldown of enemy attack by 4"));


        drawpile = new List<Card>(deck);

        drawpile = Shuffle(drawpile);
        Draw();
        handObject.GetComponent<Animator>().Play("HandUp");
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

        if (hand[cardSlot].cardType == 0)
        {
            BattleManager.bManagerInstance.player.GetComponent<Animator>().Play("Player_Attack");
        }

        else if (hand[cardSlot].cardType == 3)
        {
            // BattleManager.bManagerInstance.player.GetComponent<Animator>().Play("Player_Effect");
        }

        SortHand();

        yield return new WaitForSeconds(0.50f);

        GameObject enemy = BattleManager.bManagerInstance.enemies[enemySlot];

        enemy.transform.GetChild(0).GetComponent<EnemyScript>().enemy.health -= hand[cardSlot].power;

        BattleManager.bManagerInstance.enemies[enemySlot].transform.GetChild(0).GetComponent<Animator>().Play("OnHit");

        // yield return new WaitForSeconds(0.1f);

        BattleManager.bManagerInstance.ReduceEnemyCooldown(hand[cardSlot].cost);

        BattleManager.bManagerInstance.enemies[enemySlot] = enemy;

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

            drawpile = new List<Card>(deck);

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
