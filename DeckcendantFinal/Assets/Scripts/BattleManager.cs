using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager bManagerInstance;

    public GameObject player;

    public int playerHealth;
    public Slider healthBar;
    public TMP_Text healthText;

    public int playerCurrentBlockVal;
    public GameObject blockIcon;
    public TMP_Text blockText;

    // X values for enemy spawn points
    private static float xPosOne;
    private static float xPosTwo;
    private static float xPosThree;

    // Z value for enemy spawn points
    private static float zPos;

    private static Quaternion rotTwo;
    private static Quaternion rotThree;

    public GameObject[] enemyPrefabs;

    public GameObject[] enemies;

    public int attackingEnemies;
    public int iteration;
    public int completedAttack;
    public bool enemyTurn;
    
    private Coroutine UI;

    private Coroutine EA1;
    private Coroutine EA2;
    private Coroutine EA3;

    public TMP_Text coins;

    public Button atkStruggle;
    public Button blkStruggle;

    public Sprite atk;
    public Sprite blk;
    public Sprite heal;

    public GameObject uiBottom;
    public GameObject gameOver;

    void Awake()
    {
        if (bManagerInstance == null) { bManagerInstance = this; }
    }

    void Start()
    {
        // Setting up enemy spawn points
        xPosOne = -1.0f;
        xPosTwo = 1.5f;
        xPosThree = 4.2f;
        zPos = 6.0f;

        rotTwo = Quaternion.Euler(0.0f, 17.5f, 0.0f);
        rotThree = Quaternion.Euler(0.0f, 34.0f, 0.0f);

        for (int i = 0; i < 3; i++)
        {
            enemies[i] = null;
        }

        playerHealth = 100;
        playerCurrentBlockVal = 0;

        completedAttack = 0;
        attackingEnemies = 0;
        enemyTurn = false;
        iteration = 0;

        Spawn();

        FindStandbyEnemies();

        UpdatePlayerUI();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            enemyTurn = false;
        }
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

    public void ReduceEnemyCooldown(int cardCost)
    {
        for (int i = 0; i < 3; i++)
        {
            if(enemies[i] == null)
            {
                continue;
            }

            if (enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown > 0)
            {
                enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown -= cardCost;
            } 
        }

        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] == null)
            {
                continue;
            }

            if (enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown == 0)
            {
                EnemyTurn();
                break;
            }
        }

        int count;
        count = 0;

        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] == null)
            {
                count++;
            }

            else if (enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown == -1)
            {
                count++;
            }
        }

        if (count == 3)
        {
            CardManager.cManagerInstance.handObject.GetComponent<Animator>().Play("HandDown");
            FindStandbyEnemies();
            iteration = 0;
        }
    }

    public void EnemyTurn()
    {
        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] == null)
            {
                continue;
            }

            if(enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown == 0)
            {
                attackingEnemies++;
                EnemyAnimate(i);
            }
        }
    }

    public void EnemyAnimate(int enemy)
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(enemies[i]);
        }

        if (enemies[0] == null)
        {
            // ...
        }
        else if (enemy == 0)
        {
            iteration++;
            EA1 = StartCoroutine(EnemyAnimateCo(enemy, iteration));
        }

        if (enemies[1] == null)
        {
            // ...
        }
        else if (enemy == 1)
        {
            iteration++;
            EA2 = StartCoroutine(EnemyAnimateCo(enemy, iteration));
        }

        if (enemies[2] == null)
        {
            // ...
        }
        else if (enemy == 2)
        {
            iteration++;
            EA3 = StartCoroutine(EnemyAnimateCo(enemy, iteration));
        }
    }

    private IEnumerator EnemyAnimateCo(int enemy, int iteration)
    {
            enemyTurn = true;

            if (iteration == 1)
            {
                yield return new WaitForSeconds(1.00f);
                completedAttack++;
            }

            if (iteration == 2)
            {
                yield return new WaitForSeconds(2.00f);
                completedAttack++;
            }

            if (iteration == 3)
            {
                yield return new WaitForSeconds(3.00f);
                completedAttack++;
            }

            Enemy current = enemies[enemy].transform.GetChild(0).GetComponent<EnemyScript>().enemy;

            if (current.currentMove.type == 0)
            {
                enemies[enemy].transform.GetChild(0).GetComponent<Animator>().Play("Attack");
                yield return new WaitForSeconds(0.50f);

                player.GetComponent<Animator>().Play("Player_OnHit");

                if (playerCurrentBlockVal > 0)
                {
                    if ((playerCurrentBlockVal - current.currentMove.power) >= 0)
                    {
                        playerCurrentBlockVal -= current.currentMove.power;
                    }
                    else
                    {
                        playerHealth -= (current.currentMove.power - playerCurrentBlockVal);
                        playerCurrentBlockVal = 0;
                    }
                }
                else
                {
                    playerHealth -= current.currentMove.power;
                }

            yield return new WaitForSeconds(0.50f);

            player.GetComponent<Animator>().Play("Player_OnHit");

            }

            else if (current.currentMove.type == 1)
            {
                
                
                enemies[enemy].transform.GetChild(0).GetChild(1).GetComponent<Animator>().Play("Block");
                current.currentBlockVal += current.currentMove.power;
            }

            else if (current.currentMove.type == 2)
            {
                enemies[enemy].transform.GetChild(0).GetChild(1).GetComponent<Animator>().Play("Heal");
                current.health += current.currentMove.power;
            }

            if (enemy == 0)
            {
                enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().enabled = false;

                current.cooldown = -1;
                StopCoroutine(EA1);
            }

            else if (enemy == 1)
            {
                enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().enabled = false;

                current.cooldown = -1;
                StopCoroutine(EA2);
            }

            else if (enemy == 2)
            {
                enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().enabled = false;

                current.cooldown = -1;
                StopCoroutine(EA3);
            }
        Debug.Log("YEET");

        StartCoroutine(FinishEnemyTurn());
    }

    private IEnumerator FinishEnemyTurn()
    {
        yield return new WaitForSeconds(2.0f);

        if(completedAttack == attackingEnemies)
        {
            iteration = 0;
            attackingEnemies = 0;
            completedAttack = 0;
            enemyTurn = false;
        }


        int count;
        count = 0;

        for (int i = 0; i < 3; i++)
        {
            if (enemies[i] == null)
            {
                count++;
            }

            else if (enemies[i].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown == -1)
            {
                count++;
            }
        }

        if (count == 3)
        {
            CardManager.cManagerInstance.handObject.GetComponent<Animator>().Play("HandDown");
            FindStandbyEnemies();
            iteration = 0;
        }

        StopCoroutine(FinishEnemyTurn());
    }

    public void FindStandbyEnemies()
    {
        if(enemies[0] == null)
        {

        }
        else if (enemies[0].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown == -1)
        {
            GenerateEnemyMove(0);
        }

        if (enemies[1] == null)
        {

        }
        else if (enemies[1].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown == -1)
        {
            GenerateEnemyMove(1);
        }

        if (enemies[2] == null)
        {

        }
        else if (enemies[2].transform.GetChild(0).GetComponent<EnemyScript>().enemy.cooldown == -1)
        {
            GenerateEnemyMove(2);
        }
    }

    public void GenerateEnemyMove(int enemy)
    {
        float movePct = Random.Range(0.00f, 1.00f);

        Enemy currentClass = enemies[enemy].transform.GetChild(0).GetComponent<EnemyScript>().enemy;

        float moveWeightOne = currentClass.moves[0].chance;
        float moveWeightTwo = currentClass.moves[0].chance + currentClass.moves[1].chance;
        float moveWeightThree = moveWeightTwo + currentClass.moves[2].chance;

        if (movePct <= moveWeightOne)
        {
            currentClass.currentMove = currentClass.moves[0];
            currentClass.cooldown = currentClass.moves[0].cooldown;

            enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = currentClass.cooldown.ToString();
        }

        else if (movePct > moveWeightOne && movePct <= moveWeightTwo)
        {
            currentClass.currentMove = currentClass.moves[1];
            currentClass.cooldown = currentClass.moves[1].cooldown;

            
            enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = currentClass.cooldown.ToString();
        }

        else if (movePct > moveWeightTwo && movePct <= moveWeightThree)
        {
            currentClass.currentMove = currentClass.moves[2];
            currentClass.cooldown = currentClass.moves[2].cooldown;

            enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = currentClass.cooldown.ToString();
        }

        else if (movePct >= moveWeightThree)
        {
            currentClass.currentMove = currentClass.moves[3];
            currentClass.cooldown = currentClass.moves[3].cooldown;

            enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = currentClass.cooldown.ToString();
        }

        enemies[enemy].transform.GetChild(0).GetComponent<EnemyScript>().enemy = currentClass;

        if(enemies[enemy].transform.GetChild(0).GetComponent<EnemyScript>().enemy.currentMove.type == 0)
        {
            enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().enabled = true;
            enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().sprite = atk;
        }
        else if((enemies[enemy].transform.GetChild(0).GetComponent<EnemyScript>().enemy.currentMove.type == 1))
        {
            enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().enabled = true;
            enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().sprite = blk;
        }
        else
        {
            enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().enabled = true;
            enemies[enemy].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().sprite = heal;
        }
    }

    public void Spawn()
    {
        int enemyAmount = Random.Range(3, 4);

        for (int i = 0; i < 3; i++)
        {
            int enemy = Random.Range(0, 3);

            SpawnEnemy(i+1, i);
        }
    }

    public void SpawnEnemy(int spawnPoint, int enemy)
    {
         if (spawnPoint == 1)
         {
            enemies[0] = Instantiate(enemyPrefabs[enemy], new Vector3(xPosOne, enemyPrefabs[enemy].transform.position.y, zPos), Quaternion.identity);
            enemies[0].transform.GetChild(0).GetComponent<EnemyScript>().enemy = (ReturnEnemy(enemy, 0));
        }

         else if (spawnPoint == 2)
         {
            enemies[1] = Instantiate(enemyPrefabs[enemy], new Vector3(xPosTwo, enemyPrefabs[enemy].transform.position.y, zPos), rotTwo);
            enemies[1].transform.GetChild(0).GetChild(2).GetComponent<RectTransform>().rotation = Quaternion.identity;
            enemies[1].transform.GetChild(0).GetComponent<EnemyScript>().enemy = (ReturnEnemy(enemy, 1));
        }

         else if (spawnPoint == 3)
         {
            enemies[2] = Instantiate(enemyPrefabs[enemy], new Vector3(xPosThree, enemyPrefabs[enemy].transform.position.y, zPos), rotThree);
            enemies[2].transform.GetChild(0).GetChild(2).GetComponent<RectTransform>().rotation = Quaternion.identity;
            enemies[2].transform.GetChild(0).GetComponent<EnemyScript>().enemy = (ReturnEnemy(enemy, 2));
        }
    }

    public Enemy ReturnEnemy(int enemy, int slot)
    {
        if(enemy == 0)
        {
            return new Enemy(slot, 21, -1, null, 0, false, lipsMoves);
        }

        else if(enemy == 1)
        {
            return new Enemy(slot, 16, -1, null, 0, false, blueBoiMoves);
        }

        else if (enemy == 2)
        {
            return new Enemy(slot, 26, -1, null, 0, false, tallShroomMoves);
        }

        return null;
    }

    public void UpdatePlayerUI()
    {
        UI = StartCoroutine(RefreshUI());
    }

    private IEnumerator RefreshUI()
    {
        while (true)
        {
            if (playerHealth <= 0)
            {
                Destroy(player);

                uiBottom.SetActive(false);
                Hand.handInstance.gameObject.SetActive(false);
                gameOver.SetActive(true);
            }

            if (enemies[0] == null && enemies[1] == null && enemies[2] == null)
            {
                uiBottom.SetActive(false);
                Hand.handInstance.gameObject.SetActive(false);
                gameOver.SetActive(true);
            }

            if(playerCurrentBlockVal > 20)
            {
                blockIcon.SetActive(true);
                playerCurrentBlockVal = 20;
                blockText.text = playerCurrentBlockVal.ToString();
            }
            else if (playerCurrentBlockVal > 0)
            {
                blockIcon.SetActive(true);
                blockText.text = playerCurrentBlockVal.ToString();
            }
            else
            {
                blockIcon.SetActive(false);
                playerCurrentBlockVal = 0;
                blockText.text = playerCurrentBlockVal.ToString();
            }

            if (playerHealth > 100)
            {
                playerHealth = 100;
                healthBar.value = playerHealth;
                healthText.text = "HP: " + playerHealth.ToString();
            }

            else
            {
                healthBar.value = playerHealth;
                healthText.text = "HP: " + playerHealth.ToString();
            }

            coins.text = CoinandScore.coinsAndScoreInstance.coins.ToString();

            yield return null;
        }
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
            new EnemyMove(0, 15, 2, 0.55f), // Slap
            new EnemyMove(1, 10, 2, 0.15f), // Block
            new EnemyMove(2, 10, 3, 0.15f), // Heal 
            new EnemyMove(0, 45, 5, 0.15f), // Big Punch 
    };

    public static EnemyMove[] tallShroomMoves = new EnemyMove[]
    {
            new EnemyMove(0, 15, 2, 0.35f), // Ball Shake
            new EnemyMove(1, 5, 3, 0.30f), // Stalk Strengthen
            new EnemyMove(1, 10, 4, 0.20f), // Tall Ball Wall
            new EnemyMove(2, 20, 4, 0.15f), // Heal Spore 
    };

    public static EnemyMove[] blueBoiMoves = new EnemyMove[]
    {
            new EnemyMove(0, 10, 1, 0.40f), // Singe
            new EnemyMove(0, 25, 2, 0.30f), // Hex
            new EnemyMove(0, 65, 4, 0.15f), // Fireball
            new EnemyMove(2, 10, 2, 0.15f), // Heal
    };


    /*Type:
     * 1 = Attack
     * 2 = Block
     * 3 = Heal
     */

}