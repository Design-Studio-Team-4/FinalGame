using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public static BattleManager bManagerInstance;

    public GameObject player;

    public int playerHealth;
    public bool playerIsBlocking;
    public int playerCurrentBlockVal;

    // X values for enemy spawn points
    private static float xPosOne;
    private static float xPosTwo;
    private static float xPosThree;

    // Z value for enemy spawn points
    private static float zPos;

    public GameObject[] enemyPrefabs;

    public GameObject[] enemies;

    void Awake()
    {
        if (bManagerInstance == null) { bManagerInstance = this; }
    }

    void Start()
    {
        // Setting up enemy spawn points
        xPosOne = -1.0f;
        xPosTwo = 1.75f;
        xPosThree = 4.5f;
        zPos = 6.0f;

        for (int i = 0; i < 3; i++)
        {
            enemies[i] = null;
        }

        playerHealth = 100;
        playerIsBlocking = false;
        playerCurrentBlockVal = 0;

        Spawn();

        FindStandbyEnemies();
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

    public void FindStandbyEnemies()
    {
            if (enemies[0].GetComponent<EnemyScript>().enemy.cooldown == -1)
            {
                GenerateEnemyMove(0);
            }

            if (enemies[1].GetComponent<EnemyScript>().enemy.cooldown == -1)
            {
                GenerateEnemyMove(1);
            }

            if (enemies[2].GetComponent<EnemyScript>().enemy.cooldown == -1)
            {
                GenerateEnemyMove(2);
            }
    }

    public void GenerateEnemyMove(int enemy)
    {
        float movePct = Random.Range(0.00f, 1.00f);
        Debug.Log(movePct);

        Enemy currentClass = enemies[enemy].GetComponent<EnemyScript>().enemy;

        float moveWeightOne = currentClass.moves[0].chance;
        float moveWeightTwo = currentClass.moves[0].chance + currentClass.moves[1].chance;
        float moveWeightThree = moveWeightTwo + currentClass.moves[2].chance;

        if (movePct <= moveWeightOne)
        {
            currentClass.currentMove = currentClass.moves[0];
            currentClass.cooldown = currentClass.moves[0].cooldown;

            enemies[enemy].transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = currentClass.cooldown.ToString();
        }

        else if (movePct > moveWeightOne && movePct <= moveWeightTwo)
        {
            currentClass.currentMove = currentClass.moves[1];
            currentClass.cooldown = currentClass.moves[1].cooldown;

            
            enemies[enemy].transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = currentClass.cooldown.ToString();
        }

        else if (movePct > moveWeightTwo && movePct <= moveWeightThree)
        {
            currentClass.currentMove = currentClass.moves[2];
            currentClass.cooldown = currentClass.moves[2].cooldown;

            enemies[enemy].transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = currentClass.cooldown.ToString();
        }

        else if (movePct >= moveWeightThree)
        {
            currentClass.currentMove = currentClass.moves[3];
            currentClass.cooldown = currentClass.moves[3].cooldown;

            enemies[enemy].transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = currentClass.cooldown.ToString();
        }

        enemies[enemy].GetComponent<EnemyScript>().enemy = currentClass;

        Debug.Log(enemies[enemy].GetComponent<EnemyScript>().enemy.cooldown);
    }


    public void Spawn()
    {
        int enemyAmount = Random.Range(3, 4);

        for (int i = 0; i <= enemyAmount; i++)
        {
            int enemy = Random.Range(0, 1);

            SpawnEnemy(i, enemy);
        }
    }

    public void SpawnEnemy(int spawnPoint, int enemy)
    {
         if (spawnPoint == 1)
         {
            enemies[0] = Instantiate(enemyPrefabs[enemy], new Vector3(xPosOne, enemyPrefabs[enemy].transform.position.y, zPos), Quaternion.identity);
            enemies[0].GetComponent<EnemyScript>().enemy = (ReturnEnemy(enemy, 0));
        }

         else if (spawnPoint == 2)
         {
            enemies[1] = Instantiate(enemyPrefabs[enemy], new Vector3(xPosTwo, enemyPrefabs[enemy].transform.position.y, zPos), Quaternion.identity);
            enemies[1].GetComponent<EnemyScript>().enemy = (ReturnEnemy(enemy, 1));
        }

         else if (spawnPoint == 3)
         {
            enemies[2] = Instantiate(enemyPrefabs[enemy], new Vector3(xPosThree, enemyPrefabs[enemy].transform.position.y, zPos), Quaternion.identity);
            enemies[2].GetComponent<EnemyScript>().enemy = (ReturnEnemy(enemy, 2));
        }
    }

    public Enemy ReturnEnemy(int enemy, int slot)
    {
        if(enemy == 0)
        {
            return new Enemy(slot, 100, -1, null, 0, false, lipsMoves);
        }

        else if(enemy == 1)
        {
            return new Enemy(slot, 75, -1, null, 0, false, blueBoiMoves);
        }

        else if (enemy == 2)
        {
            return new Enemy(slot, 125, -1, null, 0, false, tallShroomMoves);
        }

        return null;
    }
    public class Enemy
    {
        public int slot;
        public int health;
        public int cooldown;
        public EnemyMove currentMove;
        public int currentBlockVal;
        public bool isBlocking;

        public EnemyMove[] moves;

        public Enemy(int s, int h, int cd, EnemyMove cm, int cbv, bool ib, EnemyMove[] em)
        {
            slot = s;
            health = h;
            cooldown = cd;
            currentMove = cm;
            currentBlockVal = cbv;
            isBlocking = ib;

            moves = em;
        }

        public Enemy(int cd)
        {
            cooldown = cd;
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
            new EnemyMove(1, 10, 2, 0.45f), // Singe (Attack/10/2/0.40)
            new EnemyMove(1, 25, 4, 0.25f), // Hex (Attack/25/4/0.20)
            new EnemyMove(1, 65, 9, 0.10f), // Fireball (Attack/65/9/0.10)
            new EnemyMove(3, 40, 3, 0.20f), // Heal (Heal/40/3/0.20)
    };


    /*Type:
     * 1 = Attack
     * 2 = Block
     * 3 = Heal
     */

}