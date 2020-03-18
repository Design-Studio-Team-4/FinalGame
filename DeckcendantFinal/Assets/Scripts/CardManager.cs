using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discard = new List<Card>();

    public Card[] hand = new Card[5];
    public GameObject handObject;
    public GameObject[] handSlots = new GameObject[5];

    public Sprite[] cardFronts;
    public Sprite[] cardBacks;
   
    void Start()
    {
       
        for (int i = 0; i < 5; i++)
        {
            hand[i] = new Card(true);
        }

        for (int i = 0; i < 2; i++)
        {
            deck.Add(new Card("Strike", 1, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
            deck.Add(new Card("Block", 2, 1, 10, false, false, cardFronts[1], cardBacks[1], "Blocks 10 damage"));
            deck.Add(new Card("First Strike", 1, 2, 7, false, true, cardFronts[2], cardBacks[0], "Deals 7 damage. If targeted enemies counter is less than 3, gain 3 block"));
            deck.Add(new Card("Hail of Daggers", 1, 4, 4, false, true, cardFronts[3], cardBacks[0], "Deal 4 damage to each enemy"));
            deck.Add(new Card("Shield Ward", 4, 3, 0, false, true, cardFronts[4], cardBacks[3], "Disregard all enemy Block on your next attack"));
            deck.Add(new Card("Soulfire Sacrifice", 1, 5, 0, false, true, cardFronts[5], cardBacks[0], "Deal 50 damage to yourself to execute an enemy"));
            deck.Add(new Card("Stasis", 4, 4, 0, false, false, cardFronts[6], cardBacks[3], "Non-boss enemies will not attack this round."));
            deck.Add(new Card("Time Steal", 4, 2, 4, false, true, cardFronts[7], cardBacks[3], "Increase the cooldown of enemy attack by 4"));
        }

        deck = Shuffle(deck);
        Draw();
        handObject.GetComponent<Animator>().Play("HandUp");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            handObject.GetComponent<Animator>().Play("HandDown");
        }
    }

    public void PlayCard(int slot)
    {   
        GameObject fCard = handSlots[slot];

        DisableCard(slot);

        fCard.GetComponent<Animator>().Play("CardHoverExit");
        fCard.GetComponent<Canvas>().sortingOrder = slot + 1;

        SortHand(); 
    }

    public void Draw()
    {
        SortHand();

        int drawCount = CheckHand();

        if (drawCount < deck.Count)
        {
            Drawing(drawCount);
        }

        else if (drawCount >= deck.Count)
        {
            if (deck.Count != 0)
            {
                Drawing(deck.Count);
                drawCount -= deck.Count;
            }
            
            for (int i = 0; i < (discard.Count); i++)
            {
                deck.Add(discard[i]);
                deck[i].used = false;
                discard.RemoveAt(i);
            }

            deck = Shuffle(deck);
            Drawing(drawCount);
        }

        SortHand();
    }

    public void Drawing(int cardsToDraw)
    {
        for (int i = 0; i < (cardsToDraw); i++)
        {
            hand[i] = deck[deck.Count - 1];
            discard.Add(hand[i]);
            deck.RemoveAt(deck.Count - 1);

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
            GameObject card = handSlots[i].transform.GetChild(0).gameObject;

            if (hand[i].used)
            {
                DisableCard(i);
            }

            else
            {
                if (hand[i].longName)
                {
                    card.transform.GetChild(3).GetComponent<TMP_Text>().fontSize = 5.75f;
                }

                else
                {
                    card.transform.GetChild(3).GetComponent<TMP_Text>().fontSize = 7.75f;
                }

                card.transform.GetChild(0).gameObject.SetActive(true);
                card.transform.GetChild(1).GetComponent<TMP_Text>().text = hand[i].desc;
                card.transform.GetChild(2).GetComponent<TMP_Text>().text = hand[i].cost.ToString();
                card.transform.GetChild(3).GetComponent<TMP_Text>().text = hand[i].name;
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

    public void DisableCard(int index)
    {
        GameObject card = handSlots[index].transform.GetChild(0).gameObject;

        card.GetComponent<Image>().sprite = hand[index].back;
        card.transform.GetChild(0).gameObject.SetActive(false);
        card.transform.GetChild(1).GetComponent<TMP_Text>().text = "";
        card.transform.GetChild(2).GetComponent<TMP_Text>().text = "";
        card.transform.GetChild(3).GetComponent<TMP_Text>().text = "";

        hand[index].used = true;
    }

    public class Card
    {
        public Sprite front;
        public Sprite back;

        public int type;
        public int power;

        public int cost;
        public string name;
        public string desc;

        public bool used;
        public bool longName;
        
        public Card(string n, int t, int c, int p, bool u, bool ln, Sprite f, Sprite b, string d)
        {
            type = t;
            power = p;
            cost = c;
            name = n;
            desc = d;
            used = u;
            longName = ln;
            front = f;
            back = b;
        }

        public Card(bool u)
        {
            used = u;
        }
    }
}
