using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StruggleTwo : MonoBehaviour
{
    // Start is called before the first frame update
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
