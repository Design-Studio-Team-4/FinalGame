using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Struggle : MonoBehaviour
{
    public Coroutine attack;

    public bool running;

    void Awake()
    {
        running = false;
    }
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
        if(running == false && Hand.handInstance.targeting == false)
        {
            BattleManager.bManagerInstance.player.GetComponent<Animator>().Play("Player_Attack");

            attack = StartCoroutine(StruggleCo());
        }
    }

    private IEnumerator StruggleCo()
    {
        running = true;

        yield return new WaitForSeconds(0.50f);

        for (int i = 0; i < 3; i++)
        {
            if (BattleManager.bManagerInstance.enemies[i] == null)
            {
                continue;
            }

            BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<Animator>().Play("OnHit");

            if (BattleManager.bManagerInstance.cower == true)
            {
                // ...
            }
            
            else if(BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.currentBlockVal > 0)
            {
                BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.currentBlockVal -= 1;
            }
            else
            {
                BattleManager.bManagerInstance.enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.health -= 1;
            }
        }

        BattleManager.bManagerInstance.ReduceEnemyCooldown(1);

        running = false;

        StopCoroutine(attack);

        
    }
}
