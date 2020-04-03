using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop shopInstance;

    public TMP_Text coins;
    public TMP_Text shopPrompt;
    public TMP_Text eggPrompt;

    void Awake()
    {
        if (shopInstance == null) { shopInstance = this; }
    }

    void Update()
    {
        coins.text = CoinandScore.coinsAndScoreInstance.coins.ToString();
    }

    public void UpdateEggPrompt(string message)
    {
        eggPrompt.text = message;
    }

    public void UpdateShopPrompt(string message)
    {
        shopPrompt.text = message;
    }
}
