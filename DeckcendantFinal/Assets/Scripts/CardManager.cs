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
    //Chris Vars  
     Vector3 Focuslocation;
    public Vector3[] OriginalLocations;
    float waitTime = 2f;
    public bool[] isMoving = new bool[5];
    public bool[] isFocus = new bool[5];
    void Start()
    {
       
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
        handObject.GetComponent<Animator>().Play("HandUp");
        for (int i = 0; i < 5; i++)
        {
            OriginalLocations[i] = handSlots[i].transform.position;
            isMoving[i] = false;
        }
        Focuslocation = new Vector3(handSlots[2].transform.position.x, handSlots[2].transform.position.y + 2, handSlots[2].transform.position.z);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerDrawAnimation();

            Debug.Log(deck.Count);
            Debug.Log(discard.Count);
        }
    }

    public void PlayCard(int slot)
    {   GameObject fCard = handSlots[slot];
        Card bCard = hand[slot];
        if (isMoving[slot]==false)
        {


        StartCoroutine(MoveCard(slot, OriginalLocations[slot]));

        isFocus[slot] = false;
        //fCard.GetComponent<Image>().sprite = hand[slot].back;
       
        //fCard.GetComponent<Animator>().Play("CardHoverExit");
        fCard.GetComponent<Canvas>().sortingOrder = slot + 1;

        bCard.used = true;
       
        SortHand(); 
        }
    }

    public void TriggerDrawAnimation()
    {
        handObject.GetComponent<Animator>().Play("HandDown");
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
    }

    public void Drawing(int cardsToDraw)
    {
        for (int i = 0; i < (cardsToDraw); i++)
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
    int findWhichSlot(GameObject CardToLookFor)
    {
        for(int i = 0; i < 5; i++)
        {
            if(handSlots[i] = CardToLookFor)
            {
                return i;
            }
        }
       
            return -1;

      
    }
    public void BringToFocus(GameObject CardToFocusOn)
    {
        if (findWhichSlot(CardToFocusOn) != -1)
        {
            int slot = findWhichSlot(CardToFocusOn);
            StartCoroutine(MoveCard(slot, Focuslocation));
            isFocus[slot] = true;
        }
        else
        {
            Debug.LogError("BringToFocus failed, findWhichSlot returned -1 ");
        }
    }
    public IEnumerator MoveCard(int SlotToMove,Vector3 TargetLocation)
    {
        Debug.Log("MoveCard Started");
        isMoving[SlotToMove] = true;
        float elapsedTime = 0f;
        while (elapsedTime < waitTime)
        {
            Debug.Log("Moving to slot");
            handSlots[SlotToMove].transform.position = Vector3.Lerp(handSlots[SlotToMove].transform.position, TargetLocation, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving[SlotToMove] = false;
        //isFocus = true;
        yield return null;
    }
    public void returnToPosition() 
    {

    }
}
