using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinandScore : MonoBehaviour
{
    public static CoinandScore coinsAndScoreInstance;

    public int coins;
    public int score;

    void Awake()
    {
        if (coinsAndScoreInstance == null) { coinsAndScoreInstance = this; }

        coins = 0;
        score = 0;

        DontDestroyOnLoad(gameObject);
    }
}
