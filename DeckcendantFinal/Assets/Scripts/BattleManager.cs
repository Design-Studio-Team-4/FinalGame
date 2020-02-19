using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public int playerHealth;
    public bool playerIsBlocking;
    public int playerCurrentBlockVal;

    public int enemyHealth;
    public int enemyCooldown;
    public bool enemyIsBlocking;
    public int enemyCurrentBlockVal;
    public EnemyMove currentEnemyMove;

    public GameObject blockUI;
    public GameObject DrawPile;
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 110;
        enemyIsBlocking = false;
        playerHealth = 100;
        playerIsBlocking = false;

        GenerateEnemyMove();
    }

    // Update is called once per frame
    void Update()
    {
       
        DrawPile.GetComponent<Deck>().Refill();
        hand.GetComponent<Hand>().Refill();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player Health: " + playerHealth);
            Debug.Log("Enemy Health: " + enemyHealth);
            Debug.Log("Enemy Cooldown: " + enemyCooldown);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerMove(1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerMove(2);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerMove(3);
        }
    }

    public void PlayerMove(int type)
    {
        Debug.Log("PlayerMove Started");


        ToggleUI(2);

        if (type == 1)
        {
            if (enemyIsBlocking)
            {
                Debug.Log("Player punched for: 20 and the enemy blocked it to reduce to damage to: " + (20 - enemyCurrentBlockVal));

                enemyHealth -= (20 - enemyCurrentBlockVal);
                enemyIsBlocking = false;
                enemyCurrentBlockVal = -1;

                enemyCooldown -= 3;
            }

            else
            {
                Debug.Log("Player punched for: 20");

                enemyHealth -= 20;

                enemyCooldown -= 3;
            }
        }

        else if (type == 2)
        {
            Debug.Log("Player blocked for: 10");
            PlayerBlock(10);

            enemyCooldown -= 2;
        }

        else
        {
            Debug.Log("Player healed for: 15");
            playerHealth += 15;

            enemyCooldown -= 4;
        }

        CheckGameState();
    }

    public void PlayerBlock(int power)
    {
        playerIsBlocking = true;
        playerCurrentBlockVal = power;
    }

    public void EnemyBlock(int power)
    {
        enemyIsBlocking = true;
        enemyCurrentBlockVal = power;
    }

    public void EnemyTurn()
    {
        if (currentEnemyMove.type == 1)
        {
            if (playerIsBlocking)
            {
                Debug.Log("Enemy punched for: " + currentEnemyMove.power + " and the player blocked it to reduce to damage to: " + (currentEnemyMove.power - playerCurrentBlockVal));

                playerHealth -= (currentEnemyMove.power - playerCurrentBlockVal);
                playerIsBlocking = false;
                playerCurrentBlockVal = -1;
            }

            else
            {
                Debug.Log("Enemy damaged player by: " + currentEnemyMove.power);

                playerHealth -= currentEnemyMove.power;
            }
        }

        else if (currentEnemyMove.type == 2)
        {
            Debug.Log("Enemy blocked for: " + currentEnemyMove.power);

            EnemyBlock(currentEnemyMove.power);
        }

        else
        {
            Debug.Log("Enemy healed for: " + currentEnemyMove.power);

            enemyHealth += currentEnemyMove.power;
        }

        GenerateEnemyMove();
        ToggleUI(1);
      }
    public void ToggleUI(int onOff)
    {
        if (onOff == 2)
        {
            blockUI.GetComponent<Image>().color = new Color32(0, 0, 0, 150);
        }
        else
        {
            blockUI.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        }
    }

    public void CheckGameState()
    {
        if (enemyCooldown <= 0)
        {
            EnemyTurn();
        }

        else
        {
            ToggleUI(1);
        }
    }

    public void GenerateEnemyMove()
    {
        float movePct = Random.Range(0.0f, 1.0f);

        if (movePct <= 0.100f )
        {
            currentEnemyMove = enemyMoveList[2];
        }

        else if( movePct >= 0.101f && movePct <= 0.250)
        {
            currentEnemyMove = enemyMoveList[1];
        }

        else if (movePct >= 0.251f && movePct <= 0.500)
        {
            currentEnemyMove = enemyMoveList[3];
        }

        else
        {
            currentEnemyMove = enemyMoveList[0];
        }

        enemyCooldown = currentEnemyMove.cooldown;
    }

    public class EnemyMove
    {
        public int type;
        public int power;
        public int cooldown;
        public float chance;

        public EnemyMove(int t, int p, int co, float ch)
        {
            type = t;
            power = p;
            cooldown = co;
            chance = ch;
        }
    }

    /*Type:
     * 1 = Attack
     * 2 = Block
     * 3 = Heal
     */
    public EnemyMove[] enemyMoveList = new EnemyMove[]
    {
        new EnemyMove(1, 15, 4, 0.50f), // Slap
        new EnemyMove(1, 45, 12, 0.15f), // Big Punch
        new EnemyMove(2, 5, 4, 0.10f), // Block
        new EnemyMove(3, 10, 6, 0.25f), // Heal
    };

}