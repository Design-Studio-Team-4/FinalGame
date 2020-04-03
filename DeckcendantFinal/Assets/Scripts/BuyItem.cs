using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static CardManager;

public class BuyItem : MonoBehaviour
{
    private string cardName;
    private int cardCost;

    private Color disabledColour;

    void Start()
    {
        cardName = transform.GetChild(0).GetComponent<TMP_Text>().text;
        cardCost = int.Parse(transform.GetChild(1).GetComponent<TMP_Text>().text);

        disabledColour = new Color(1.0f, 1.0f, 1.0f, 0.50f);
    }

    void Update()
    {
        if (cardCost > CoinandScore.coinsAndScoreInstance.coins)
        {
            GetComponent<Button>().interactable = false;

            transform.GetChild(0).GetComponent<TMP_Text>().color = disabledColour;
            transform.GetChild(2).GetComponent<TMP_Text>().color = disabledColour;
            transform.GetChild(3).GetComponent<TMP_Text>().color = disabledColour;
        }
        else
        {
            GetComponent<Button>().interactable = true;

            transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
            transform.GetChild(2).GetComponent<TMP_Text>().color = Color.white;
            transform.GetChild(3).GetComponent<TMP_Text>().color = Color.white;
        }
    }

    public void BuyOnClick()
    {
            if (cardName == "Time Steal")
            {
                cManagerInstance.deck.Add(new Card("Time Steal", 4, 2, 4, 0, false, true, cManagerInstance.cardFronts[4], cManagerInstance.cardBacks[3], "Increase the cooldown of enemy attack by 4"));
            }
            if (cardName == "Cower")
            {
                cManagerInstance.deck.Add(new Card("Cower", 1, 1, 2, 25, false, true, cManagerInstance.cardFronts[5], cManagerInstance.cardBacks[1], "Gain 25 block, attack cards you play this turn do 0 damage"));
            }
            if (cardName == "Life Drink")
            {
                cManagerInstance.deck.Add(new Card("Life Drink", 2, 1, 3, 7, false, true, cManagerInstance.cardFronts[3], cManagerInstance.cardBacks[0], "Deal 8 damage, gain 8 hp"));
            }

            Shop.shopInstance.UpdateEggPrompt("Good Choice!");
            Shop.shopInstance.UpdateShopPrompt("Added " + cardName + " to your deck!");

            CoinandScore.coinsAndScoreInstance.coins -= cardCost;

            gameObject.SetActive(false);
    }
}