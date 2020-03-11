using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> drawPile = new List<Card>();
    public List<Card> discard = new List<Card>();

    public Card[] hand = new Card[5];
    public GameObject handObject;
    public GameObject[] handSlots = new GameObject[5];

    public static Sprite[] cardFronts;
    public static Sprite[] cardBacks;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            hand[i] = new Card(true);
        }

        drawPile = Shuffle(drawPile);

        cardFronts = Resources.LoadAll<Sprite>("CardFronts");
        cardBacks = Resources.LoadAll<Sprite>("CardBacks");

        for (int i = 0; i < 30; i++)
        {
            drawPile.Add(new Card(1, 5, 1, false, cardFronts[0], cardBacks[0])); // Strike: Damage, 5 power, 1 cost.
        }

        Draw();
        handObject.GetComponent<Animator>().Play("HandUp");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerDrawAnimation();

            Debug.Log(drawPile.Count);
            Debug.Log(discard.Count);
        }
    }

    public void PlayCard(int slot)
    {
        GameObject fCard = handSlots[slot];
        Card bCard = hand[slot];

        fCard.GetComponent<Image>().sprite = hand[slot].back;
        fCard.GetComponent<Animator>().Play("CardHoverExit");
        fCard.GetComponent<Canvas>().sortingOrder = slot + 1;

        bCard.used = true;

        SortHand();
    }

    public void TriggerDrawAnimation()
    {
        handObject.GetComponent<Animator>().Play("HandDown");
    }

    public void Draw()
    {
        SortHand();

        int drawCount = CheckHand();

        if (drawCount < drawPile.Count)
        {
            Drawing(drawCount);
        }

        else if (drawCount >= drawPile.Count)
        {
            if (drawPile.Count != 0)
            {
                Drawing(drawPile.Count);
                drawCount -= drawPile.Count;
            }
            
            for (int i = 0; i < (discard.Count); i++)
            {
                drawPile.Add(discard[i]);
                drawPile[i].used = false;
                discard.RemoveAt(i);
            }

            drawPile = Shuffle(drawPile);
            Drawing(drawCount);
        }
    }

    public void Drawing(int cardsToDraw)
    {
        for (int i = 0; i < (cardsToDraw); i++)
        {
            hand[i] = drawPile[drawPile.Count - 1];
            discard.Add(hand[i]);
            drawPile.RemoveAt(drawPile.Count - 1);

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

    public void SortHand()
    {
        for (int j = 0; j <= hand.Length - 2; j++)
        {
            for (int i = 0; i <= hand.Length - 2; i++)
            {
                if (hand[i].used == false)
                {
                    Card tempCard = hand[i + 1];
                    Sprite tempSprite = handSlots[i + 1].GetComponent<Image>().sprite;

                    hand[i + 1] = hand[i];
                    handSlots[i + 1].GetComponent<Image>().sprite = handSlots[i].GetComponent<Image>().sprite;

                    hand[i] = tempCard;
                    handSlots[i].GetComponent<Image>().sprite = tempSprite;

                }
            }
        }
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
