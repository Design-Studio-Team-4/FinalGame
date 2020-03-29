using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Struggle : MonoBehaviour
{
    public Coroutine attack;

    public void ButtonPress()
    {
        if(Hand.handInstance.targeting == false)
        {
            BattleManager.bManagerInstance.player.GetComponent<Animator>().Play("Player_Attack");

            attack = StartCoroutine(StruggleCo());
        }
    }

    private IEnumerator StruggleCo()
    {
        yield return new WaitForSeconds(0.50f);

        for (int i = 0; i < 3; i++)
        {
            if (BattleManager.bManagerInstance.enemies[i] == null)
            {
                continue;
            }

            BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<Animator>().Play("OnHit");
            BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.health -= 1;
        }
        BattleManager.bManagerInstance.ReduceEnemyCooldown(1);

        StopCoroutine(attack);
    }
}
