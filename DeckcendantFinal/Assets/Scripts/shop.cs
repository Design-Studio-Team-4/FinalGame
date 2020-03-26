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
        BottomMenu.gameObject.SetActive(false);
    }

    void Start()
    {
        // initialize shop elements
        Awake();

        // get a random card for each shop slot
        ChooseButton(Purchase1);
        ChooseButton(Purchase2);
        ChooseButton(Purchase3);

    }

    // exit shop if you click exit
    //void Update()
    //{
    //    if (input = Spacebar)
    //    {
    //        ExitShop();
    //    }
    //}

    public void CreateButton(string name, int cost, int power, bool isUsed, bool hasLongName, Sprite Fronts, Sprite Backs, string d, UnityEngine.Transform purchase)
    {
        Transform PurchaseFieldTransform = Instantiate(purchase, PurchaseField);
        PurchaseFieldTransform.gameObject.SetActive(true);

        // set name, cost, and description
        PurchaseFieldTransform.Find("name").GetComponent<TextMeshProUGUI>().SetText(name);
        PurchaseFieldTransform.Find("cost").GetComponent<TextMeshProUGUI>().SetText(cost.ToString());
        PurchaseFieldTransform.Find("desc").GetComponent<TextMeshProUGUI>().SetText(d);

        // set front
        PurchaseFieldTransform.Find("Image").GetComponent<Image>().sprite = Fronts;

        //PurchaseFieldTransform.GetComponent<Button>().AddListener(BuyOnClick);

        void BuyOnClick()
        {
            Debug.Log("you clicked");
            //if (score >= cost)

            //CardManager.cManagerInstance.deck.Add(new Card(name, cost, power, isUsed, hasLongName, Fronts, Backs, d));

            // score - cost = score

        }
        // Clicked on shop button

    }

    // takes CreateButton and uses it to create each different card as a button
    public void ChooseButton(UnityEngine.Transform purchase)
    {
        // get random number to find card in the list
        var num = Random.Range(1, CARDTOTAL + 1);
        Debug.Log(num);

        // takes all normal card values plus purchase as the button to place it at
        if (num >= 11) CreateButton("Life Drink", 1, 2, false, true, CardManager.cManagerInstance.cardFronts[10], CardManager.cManagerInstance.cardBacks[0], "Increase the cooldown of enemy attack by 4", purchase);
        else if (num >= 10) CreateButton("Turn the Tides", 1, 30, false, true, CardManager.cManagerInstance.cardFronts[9], CardManager.cManagerInstance.cardBacks[0], "Deal 30 damage to each enemy. Can only be played if you have less than 10HP", purchase);
        else if (num >= 9) CreateButton("Time Steal", 4, 2, false, true, CardManager.cManagerInstance.cardFronts[8], CardManager.cManagerInstance.cardBacks[3], "Increase the cooldown of enemy attack by 4", purchase);
        else if (num >= 8) CreateButton("Stasis", 4, 4, false, false, CardManager.cManagerInstance.cardFronts[7], CardManager.cManagerInstance.cardBacks[3], "Non-boss enemies will not attack this round", purchase);
        else if (num >= 7) CreateButton("Soulfire Sacrifice", 1, 5, false, true, CardManager.cManagerInstance.cardFronts[6], CardManager.cManagerInstance.cardBacks[0], "Deal 50 damage to yourself to execute an enemy", purchase);
        else if (num >= 6) CreateButton("Shield Ward", 4, 3, false, true, CardManager.cManagerInstance.cardFronts[5], CardManager.cManagerInstance.cardBacks[3], "Disregard all enemy Block on your next attack", purchase);
        else if (num >= 5) CreateButton("Hail of Daggers", 1, 4, false, true, CardManager.cManagerInstance.cardFronts[4], CardManager.cManagerInstance.cardBacks[0], "Deal 4 damage to each enemy", purchase);
        else if (num >= 4) CreateButton("First Strike", 1, 2, false, true, CardManager.cManagerInstance.cardFronts[3], CardManager.cManagerInstance.cardBacks[0], "Deals 7 damage. If targeted enemies counter is less than 3, gain 3 block", purchase);
        else if (num >= 3) CreateButton("Heal", 1, 5, false, false, CardManager.cManagerInstance.cardFronts[2], CardManager.cManagerInstance.cardBacks[2], "Heals 5 health", purchase);
        else if (num >= 2) CreateButton("Block", 1, 10, false, false, CardManager.cManagerInstance.cardFronts[1], CardManager.cManagerInstance.cardBacks[1], "Blocks 10 damage", purchase);
        else if (num >= 1) CreateButton("Strike", 1, 5, false, false, CardManager.cManagerInstance.cardFronts[0], CardManager.cManagerInstance.cardBacks[0], "Deals 5 Damage", purchase);
    }

    //public void ExitShop()
   // {
     //   // destroy buttons
    //    // Disable shop

    //}
}