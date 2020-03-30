using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand handInstance;
    public bool targeting;

    void Awake()
    {
        if (handInstance == null) { handInstance = this; }
        targeting = false;
    }

    public void Draw()
    {
        CardManager.cManagerInstance.Draw();
    }

    public void EnemyTurnOn()
    {
        BattleManager.bManagerInstance.enemyTurn = true;
    }

    public void EnemyTurnOff()
    {
        BattleManager.bManagerInstance.enemyTurn = false;
    }
}
