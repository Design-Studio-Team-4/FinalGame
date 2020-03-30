using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StruggleTwo : MonoBehaviour
{
    void Update()
    {
        if (Hand.handInstance.targeting == true || BattleManager.bManagerInstance.enemyTurn == true)
        {
            GetComponent<Button>().enabled = false;
        }
        else
        {
            GetComponent<Button>().enabled = true;
        }
    }

    public void ButtonPress()
    {
        if (Hand.handInstance.targeting == false)
        {
            BattleManager.bManagerInstance.player.transform.GetChild(2).GetComponent<Animator>().Play("Block");

            BattleManager.bManagerInstance.playerCurrentBlockVal += 3;
            BattleManager.bManagerInstance.ReduceEnemyCooldown(1);
        } 
    }
}
