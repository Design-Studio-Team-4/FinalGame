﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public int playerHealth;
    public bool playerIsBlocking;
    public int playerCurrentBlockVal;

    public float xPosOne;
    public float xPosTwo;
    public float xPosThree;

    public Quaternion rotOne;
    public Quaternion rotTwo;
    public Quaternion rotThree;

    public float yPos;
    public float zPosOne;
    public float zPosTwo;

    public GameObject lipsPrefab;
    public GameObject tallShroomPrefab;
    public GameObject blueBoiPrefab;

    public Enemy[] enemies;

    void Start()
    {
        xPosOne = -1.5f;
        xPosTwo = 1.5f;
        xPosThree = 4.4f;
        yPos = 1.0f;
        zPosOne = 6.0f;
        zPosTwo = 7.5f;

        rotOne = Quaternion.Euler(0, 5, 0);
        rotTwo = Quaternion.Euler(0, 25, 0);
        rotThree = Quaternion.Euler(0, 45, 0);

        enemies = new Enemy[]
        {
            new Enemy(100, 0, 0, false, lipsPrefab, lipsMoves),
            new Enemy(135, 0, 0, false, tallShroomPrefab, tallShroomMoves),
            new Enemy(75, 0, 0, false, blueBoiPrefab, blueBoiMoves),
         };

    Debug.Log(enemies[0].prefab);

        playerHealth = 100;
        playerIsBlocking = false;

        Spawn();

        //GenerateEnemyMove();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerMove(int type)
    {
        ToggleUI(2);

        /*
         
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

        */
    }

    public void PlayerBlock(int power)
    {
        playerIsBlocking = true;
        playerCurrentBlockVal = power;
    }

    public void EnemyBlock(int power)
    {
        
    }

    public void EnemyTurn()
    {
        /*

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

        */
    }
    public void ToggleUI(int onOff)
    {
        if (onOff == 2)
        {

        }
        else
        {

        }
    }

    public void CheckGameState()
    {
        
    }

    public void GenerateEnemyMove()
    {
        float movePct = Random.Range(0.0f, 1.0f);

        if (movePct <= 0.100f)
        {
            // currentEnemyMove = enemyMoveList[2];
        }

        else if (movePct >= 0.101f && movePct <= 0.250)
        {
            // currentEnemyMove = enemyMoveList[1];
        }

        else if (movePct >= 0.251f && movePct <= 0.500)
        {
            // currentEnemyMove = enemyMoveList[3];
        }

        else
        {
            // currentEnemyMove = enemyMoveList[0];
        }

        // enemyCooldown = currentEnemyMove.cooldown;
    }

    public void Spawn()
    {
        int enemyAmount = Random.Range(3, 4);

        for (int i = 0; i <= enemyAmount; i++)
        {
            int enemy = Random.Range(0, 2);

            SpawnEnemy(i, enemy);
        }
    }

    public void SpawnEnemy(int spawnPoint, int enemy)
    {
        if (spawnPoint == 1)
        {
           Instantiate(enemies[enemy].prefab, new Vector3(xPosOne, enemies[enemy].prefab.transform.position.y, zPosOne), rotOne);
        }

        else if (spawnPoint == 2)
        {
            Instantiate(enemies[enemy].prefab, new Vector3(xPosTwo, enemies[enemy].prefab.transform.position.y, zPosTwo), rotTwo);
        }

        else if (spawnPoint == 3)
        {
            Instantiate(enemies[enemy].prefab, new Vector3(xPosThree, enemies[enemy].prefab.transform.position.y, zPosOne), rotThree);
        }
    }

    public class Enemy
    {
        public int health;
        public int cooldown;
        public int currentBlockVal;
        public bool isBlocking;

        public GameObject prefab;

        public EnemyMove[] moves;

        public Enemy(int h, int cd, int cbv, bool ib, GameObject p, EnemyMove[] em)
        {
            health = h;
            cooldown = cd;
            currentBlockVal = cbv;
            isBlocking = ib;
            prefab = p;

            moves = em;
        }
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

    public static EnemyMove[] lipsMoves = new EnemyMove[]
    {
            new EnemyMove(1, 15, 2, 0.50f), // Slap (Attack/15/2/0.50)
            new EnemyMove(2, 10, 2, 0.10f), // Block (Block/10/2/0.10)
            new EnemyMove(3, 10, 3, 0.25f), // Heal (Heal/10/3/0.25)
            new EnemyMove(1, 45, 6, 0.15f), // Big Punch (Attack/45/6/0.15)
    };

    public static EnemyMove[] tallShroomMoves = new EnemyMove[]
    {
            new EnemyMove(1, 10, 4, 0.20f), // Ball Shake (Attack/10/4/0.20)
            new EnemyMove(2, 15, 2, 0.30f), // Stalk Strengthen (Defend/15/2/0.30)
            new EnemyMove(2, 35, 4, 0.35f), // Tall Ball Wall (Defend/35/4/0.35)
            new EnemyMove(3, 60, 8, 0.15f), // Heal Spore (Heal/60/8/0.15)
    };

    public static EnemyMove[] blueBoiMoves = new EnemyMove[]
    {
            new EnemyMove(1, 10, 2, 0.40f), // Singe (Attack/10/2/0.40)
            new EnemyMove(1, 25, 4, 0.20f), // Hex (Attack/25/4/0.20)
            new EnemyMove(1, 65, 9, 0.10f), // Fireball (Attack/65/9/0.10)
            new EnemyMove(3, 40, 3, 0.20f), // Heal (Heal/40/3/0.20)
            new EnemyMove(3, 70, 7, 0.10f), // Big Heal (Heal/70/7/0.10)
    };


    /*Type:
     * 1 = Attack
     * 2 = Block
     * 3 = Heal
     */

}