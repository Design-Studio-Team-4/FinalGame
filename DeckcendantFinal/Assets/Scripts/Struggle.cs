using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Struggle : MonoBehaviour
{
    public Coroutine attack;

    void Update()
    {
        if(Hand.handInstance.targeting == true || BattleManager.bManagerInstance.enemyTurn == true)
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
        BattleManager.bManagerInstance.player.GetComponent<Animator>().Play("Player_Attack");

        attack = StartCoroutine(StruggleCo());
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
