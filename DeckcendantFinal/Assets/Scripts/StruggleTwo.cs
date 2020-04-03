using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StruggleTwo : MonoBehaviour
{
    public Coroutine block;

    public bool running;

    void Awake()
    {
        running = false;
    }
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
        if (running == false && Hand.handInstance.targeting == false)
        {
            BattleManager.bManagerInstance.player.transform.GetChild(2).GetComponent<Animator>().Play("Block");

            block = StartCoroutine(StruggleCo());
        }
    }

    private IEnumerator StruggleCo()
    {
        running = true;

        BattleManager.bManagerInstance.playerCurrentBlockVal += 3;
        BattleManager.bManagerInstance.ReduceEnemyCooldown(1);

        yield return new WaitForSeconds(0.50f);

        running = false;

        StopCoroutine(block);
    }
}
