using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public int playerHealth;
    public bool playerIsBlocking;
    public int playerCurrentBlockVal;

    public GameObject enemyOne;
    public GameObject enemyTwo;
    public GameObject enemyThree;

    public static GameObject[] enemyPrefabs;

   /* public Enemy[] enemies = new Enemy[]
    {
        new Enemy(100, 0, 0, false, enemyPrefabs[0], enemyPrefabs[0].GetComponent<Moves>)
    }; */

    void Start()
    {

        enemyPrefabs = Resources.LoadAll<GameObject>("EnemyPrefabs");

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
            int enemy = Random.Range(0, 3);

            SpawnEnemy(i, enemy);
        }
    }

    public void SpawnEnemy(int spawnPoint, int enemy)
    {
        if (spawnPoint == 1)
        {
            Instantiate(enemyPrefabs[enemy], new Vector3(-1.5f, 1.0f + enemyPrefabs[enemy].transform.position.y, 6.0f), Quaternion.identity, enemyOne.transform);
        }

        else if (spawnPoint == 2)
        {
            Instantiate(enemyPrefabs[enemy], new Vector3(1.5f, 1.0f + enemyPrefabs[enemy].transform.position.y, 6.0f), Quaternion.identity, enemyTwo.transform);
        }

        else if (spawnPoint == 3)
        {
            Instantiate(enemyPrefabs[enemy], new Vector3(4.5f, 1.0f + enemyPrefabs[enemy].transform.position.y, 6.0f), Quaternion.identity, enemyThree.transform);
        }
    }

    public class Enemy
    {
        public int health;
        public int cooldown;
        public int currentBlockVal;
        public bool isBlocking;

        public GameObject prefab;

        public EnemyMove[] enemyMoves;

        public Enemy(int h, int cd, int cbv, bool ib, GameObject p, EnemyMove[] em)
        {
            health = h;
            cooldown = cd;
            currentBlockVal = cbv;
            isBlocking = ib;
            prefab = p;

            enemyMoves = em;
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
    }
}