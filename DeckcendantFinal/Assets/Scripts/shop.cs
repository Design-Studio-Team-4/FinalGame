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
    public float CARDTOTAL = 10;

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
    }

    void Start()
    {
        Awake();

        ChooseButton(Purchase1);
        ChooseButton(Purchase2);
        ChooseButton(Purchase3);

    }

    public void CreateButton(Sprite cardFronts, string name, int cost, UnityEngine.Transform purchase)
    {
        Transform PurchaseFieldTransform = Instantiate(purchase, PurchaseField);
        PurchaseFieldTransform.gameObject.SetActive(true);

        // set name and cost
        PurchaseFieldTransform.Find("name").GetComponent<TextMeshProUGUI>().SetText(name);
        PurchaseFieldTransform.Find("cost").GetComponent<TextMeshProUGUI>().SetText(cost.ToString());

        // set front
        PurchaseFieldTransform.Find("Image").GetComponent<Image>().sprite = cardFronts;

        PurchaseFieldTransform.GetComponent<Button>();
    }

    public void ChooseButton(UnityEngine.Transform purchase)
    {
        var num = Random.Range(1, CARDTOTAL + 1);
        Debug.Log(num);

        if (num >= 10) CreateButton(CardManager.cManagerInstance.cardFronts[9], "Turn The Tides", 3, purchase);
        else if (num >= 9) CreateButton(CardManager.cManagerInstance.cardFronts[8], "Time Steal", 3, purchase);
        else if (num >= 8) CreateButton(CardManager.cManagerInstance.cardFronts[7], "Stasis", 3, purchase);
        else if (num >= 7) CreateButton(CardManager.cManagerInstance.cardFronts[6], "Soulfire Sacrifice", 3, purchase);
        else if (num >= 6) CreateButton(CardManager.cManagerInstance.cardFronts[5], "Shield Ward", 2, purchase);
        else if (num >= 5) CreateButton(CardManager.cManagerInstance.cardFronts[4], "Hail Of Daggers", 3, purchase);
        else if (num >= 4) CreateButton(CardManager.cManagerInstance.cardFronts[3], "First Strike", 3, purchase);
        else if (num >= 3) CreateButton(CardManager.cManagerInstance.cardFronts[2], "Heal", 3, purchase);
        else if (num >= 2) CreateButton(CardManager.cManagerInstance.cardFronts[1], "Block", 2, purchase);
        else if (num >= 1) CreateButton(CardManager.cManagerInstance.cardFronts[0], "Strike", 1, purchase);
        
      

    }

    //public void OnClick
    //{
    //    //if (score => cost)
    //    //{
    //        // give player card
    //        //CardManager.cManagerInstance.deck.Add(new Card("Heal", 1, 5, false, false, cardFronts[2], cardBacks[2], "Heals 5 health"));
    //        // subtract score
    //        //score - cost = score;
    //        // remove button
    //    //}
    //}
    
}