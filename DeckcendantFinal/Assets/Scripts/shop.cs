// all comands that will need to take place once the shop awakes
// must be updated as new cards are introduced ( CARDTOTAL and ChoseButton )

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class shop : MonoBehaviour
{

    public Transform Purchase1;
    public Transform Purchase2;
    public Transform Purchase3;
    public Transform PurchaseField;
    public Transform BottomMenu;
    public float CARDTOTAL = 11;
    public Transform ExitShop;

    // initiates shop elements
    public void Awake()
    {
        PurchaseField = transform.Find("PurchaseField");
        Purchase1 = PurchaseField.Find("Purchase1");
        Purchase1.gameObject.SetActive(false);
        Purchase2 = PurchaseField.Find("Purchase2");
        Purchase2.gameObject.SetActive(false);
        Purchase3 = PurchaseField.Find("Purchase3");
        Purchase3.gameObject.SetActive(false);
        ExitShop = PurchaseField.Find("ExitShop");
        BottomMenu.gameObject.SetActive(false);

    }

    void Start()
    {

        // if button to go to shop is pressed
        // initialize shop elements
        Awake();

        // get a random card for each shop slot
        ChooseButton(Purchase1);
        ChooseButton(Purchase2);
        ChooseButton(Purchase3);

    }

    public void CreateButton(string name, int cost, bool hasLongName, Sprite Fronts, string d, UnityEngine.Transform purchase)
    {
        Transform PurchaseFieldTransform = Instantiate(purchase, PurchaseField);
        PurchaseFieldTransform.gameObject.SetActive(true);

        // set font size for name
        if (hasLongName == true) PurchaseFieldTransform.Find("name").GetComponent<TextMeshProUGUI>().fontSize = 5.75f;       
        if (hasLongName == false) PurchaseFieldTransform.Find("name").GetComponent<TextMeshProUGUI>().fontSize = 7.75f;

        // set name, cooldown, cost, and description
        PurchaseFieldTransform.Find("name").GetComponent<TextMeshProUGUI>().SetText(name);
        PurchaseFieldTransform.Find("cost").GetComponent<TextMeshProUGUI>().SetText(cost.ToString());
        PurchaseFieldTransform.Find("Cooldown").GetComponent<TextMeshProUGUI>().SetText(cost.ToString());
        PurchaseFieldTransform.Find("desc").GetComponent<TextMeshProUGUI>().SetText(d);

        //set front
        PurchaseFieldTransform.Find("Image").GetComponent<Image>().sprite = Fronts;

    }

    // takes CreateButton and uses it to create each different card as a button
    public void ChooseButton(UnityEngine.Transform purchase)
    {
        // get random number to find card in the list
        var num = Random.Range(1, CARDTOTAL + 1);
        Debug.Log(num);

        // takes all normal card values plus purchase as the button to place it at
        if (num >= 11) CreateButton("Life Drink", 1, true, CardManager.cManagerInstance.cardFronts[10], "Increase the cooldown of enemy attack by 4", purchase);
        else if (num >= 10) CreateButton("Turn the Tides", 1, true, CardManager.cManagerInstance.cardFronts[9], "Deal 30 damage to each enemy. Can only be played if you have less than 10HP", purchase);
        else if (num >= 9) CreateButton("Time Steal", 4, true, CardManager.cManagerInstance.cardFronts[8], "Increase the cooldown of enemy attack by 4", purchase);
        else if (num >= 8) CreateButton("Stasis", 4, false, CardManager.cManagerInstance.cardFronts[7], "Non-boss enemies will not attack this round", purchase);
        else if (num >= 7) CreateButton("Soulfire Sacrifice", 1, true, CardManager.cManagerInstance.cardFronts[6], "Deal 50 damage to yourself to execute an enemy", purchase);
        else if (num >= 6) CreateButton("Shield Ward", 4, true, CardManager.cManagerInstance.cardFronts[5], "Disregard all enemy Block on your next attack", purchase);
        else if (num >= 5) CreateButton("Hail of Daggers", 1, true, CardManager.cManagerInstance.cardFronts[4], "Deal 4 damage to each enemy", purchase);
        else if (num >= 4) CreateButton("First Strike", 1, true, CardManager.cManagerInstance.cardFronts[3], "Deals 7 damage. If targeted enemies counter is less than 3, gain 3 block", purchase);
        else if (num >= 3) CreateButton("Heal", 1, false, CardManager.cManagerInstance.cardFronts[2], "Heals 5 health", purchase);
        else if (num >= 2) CreateButton("Block", 1, false, CardManager.cManagerInstance.cardFronts[1], "Blocks 10 damage", purchase);
        else if (num >= 1) CreateButton("Strike", 1, false, CardManager.cManagerInstance.cardFronts[0], "Deals 5 Damage", purchase);
    }

    
    // OnClick method for the card buttons
    public void BuyOnClick(TextMeshProUGUI name)
    {
        string cardname = name.text.ToString();
        
        //GameObject.Find("name").GetComponent<TextMeshProUGUI>().SetText(name);
        Debug.Log("this card is");
        Debug.Log(cardname);

        if (cardname == "Strike") CardManager.cManagerInstance.deck.Add(new Card("Strike", 0, 1, 5, false, false, cardFronts[0], cardBacks[0], "Deals 5 Damage"));
        if (cardname == "Block") CardManager.cManagerInstance.deck.Add(new Card("Block", 1, 1, 1, 10, false, false, cardFronts[1], cardBacks[1], "Blocks 10 damage"));
        if (cardname == "Heal") CardManager.cManagerInstance.deck.Add(new Card("Heal", 2, 1, 1, 5, false, false, cardFronts[2], cardBacks[2], "Heals 5 health"));
        if (cardname == "First Strike") CardManager.cManagerInstance.deck.Add(new Card("First Strike", 1, 2, 7, false, true, cardFronts[3], cardBacks[0], "Deals 7 damage. If targeted enemies counter is less than 3, gain 3 block"));
        if (cardname == "Hail of Daggers") CardManager.cManagerInstance.deck.Add(new Card("Hail of Daggers", 1, 4, 4, false, true, cardFronts[4], cardBacks[0], "Deal 4 damage to each enemy"));
        if (cardname == "Shield Ward") CardManager.cManagerInstance.deck.Add(new Card("Shield Ward", 4, 3, 0, false, true, cardFronts[5], cardBacks[3], "Disregard all enemy Block on your next attack"));
        if (cardname == "Soulfire Sacrifice") CardManager.cManagerInstance.deck.Add(new Card("Soulfire Sacrifice", 1, 5, 0, false, true, cardFronts[6], cardBacks[0], "Deal 50 damage to yourself to execute an enemy"));
        if (cardname == "Stasis") CardManager.cManagerInstance.deck.Add(new Card("Soulfire Sacrifice", 1, 5, 0, false, true, cardFronts[6], cardBacks[0], "Deal 50 damage to yourself to execute an enemy"));
        if (cardname == "Time Steal") CardManager.cManagerInstance.deck.Add(new Card("Stasis", 4, 4, 0, false, false, cardFronts[7], cardBacks[3], "Non-boss enemies will not attack this round."));
        if (cardname == "Turn the Tides") CardManager.cManagerInstance.deck.Add(new Card("Turn the Tides", 1, 30, 0, false, true, cardFronts[9], cardBacks[0], "Deal 30 damage to each enemy. Can only be played if you have less than 10HP"));
        if (cardname == "Life Drink") CardManager.cManagerInstance.deck.Add(new Card("Life Drink", 1, 2, 8, false, true, cardFronts[10], cardBacks[0], "Increase the cooldown of enemy attack by 4"));

        //.gameObject.SetActive(true);

    }
    // Clicked on shop button

    // when exitshop button is pressed
    public void ShopLeave()
    {
        // destroy buttons
        // Disable shop
        Purchase1.gameObject.SetActive(false);
        Purchase2.gameObject.SetActive(false);
        Purchase3.gameObject.SetActive(false);
        BottomMenu.gameObject.SetActive(false);
        ExitShop.gameObject.SetActive(false);
        BottomMenu.gameObject.SetActive(true);

    }
}