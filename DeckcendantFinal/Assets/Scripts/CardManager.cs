using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discard = new List<Card>();

    public Card[] hand = new Card[5];
    public GameObject handObject;
    public GameObject[] handSlots;

    public static Sprite[] cardFronts;
    public static Sprite[] cardBacks;

    void Start()
    {
        handObject.GetComponent<Animator>().Play("HandUp");

        for (int i = 0; i < 5; i++)
        {
            hand[i] = new Card(true);
        }

        deck = Shuffle(deck);

        cardFronts = Resources.LoadAll<Sprite>("CardFronts");
        cardBacks = Resources.LoadAll<Sprite>("CardBacks");

        for (int i = 0; i < 30; i++)
        {
            deck.Add(new Card(1, 5, 1, false, cardFronts[0], cardBacks[0])); // Strike: Damage, 5 power, 1 cost.
        }

        Draw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw()
    {
        int drawCount = CheckHand();

        for (int i = (5 - drawCount); i < 5; i++)
        {
            hand[i] = deck[deck.Count - 1];
            discard.Add(hand[i]);
            deck.RemoveAt(deck.Count - 1);

           handSlots[i].GetComponent<Image>().sprite = hand[i].front;
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

    public List<Card> Shuffle(List<Card> pile)
    {
        for (int i = 0; i < pile.Count; i++)
        {
            Card temp = pile[i];
            int random = Random.Range(i, pile.Count);
            pile[i] = pile[random];
            pile[random] = temp;
        }

        return pile;
    }

    public class Card
    {
        public Sprite front;
        public Sprite back;

        public int type;
        public int power;
        public int cost;
        public bool used;

        public Card(int t, int p, int c, bool u, Sprite f, Sprite b)
        {
            type = t;
            power = p;
            cost = c;
            used = u;
            front = f;
            back = b;
        }

        public Card(bool u)
        {
            used = u;
        }
    }
}
