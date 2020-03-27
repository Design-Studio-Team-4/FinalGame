using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public void NextScene()
    {
        CoinandScore.coinsAndScoreInstance.coins = 0;
        CoinandScore.coinsAndScoreInstance.coins = 0;

        SceneManager.LoadScene(0);
    }
}
