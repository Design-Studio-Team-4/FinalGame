using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public class Enemy
    {
        public string name;
        public int type;
        public int maxHealth;
        public int currentHealth;
        public int block;
        public int cooldown;

        EnemyMoves.Move currentMove = new EnemyMoves.Move();

        private static System.Random rand = new System.Random();

        public Enemy()
        {
            name = "Enemy Name";
            type = 0;
            maxHealth = 10;
            currentHealth = maxHealth;
            block = 0;
            cooldown = 0;
        }

        public Enemy(string n, int t, int hp)
        {
            name = n;
            type = t;
            maxHealth = hp;
            currentHealth = maxHealth;
            block = 0;
            cooldown = 0;
        }

        public void TakeDamage(Crd c)
        {
            if (block - c.power <= 0) currentHealth -= block + c.power;
            else block -= c.power;
        }

        public void ChooseMove()
        {
            int i = rand.Next(/* NUMBER OF MOVES*/ 3);
            currentMove = EnemyMoves.instance.moveList[i];
            cooldown += currentMove.cost;
        }

        public void TakeAction()
        {
            //execute move
            switch (currentMove.type)
            {
                case 0: // if attack
                    Player.instance.TakeDamage(currentMove.power); // *** change instance to the actual player 
                    break;
                case 1: // if block
                    block += currentMove.power;
                    break;
                case 2: // if heal
                    if (currentHealth + currentMove.power > maxHealth) currentHealth = maxHealth;
                    else currentHealth += currentMove.power;
                    break;
            }

            //choose move
            ChooseMove();

        }
    }

    public Enemy[] enemyList = new Enemy[]
    {
        //         Name         Type    Health
        new Enemy("JohnEgg",    50,     100000),
        new Enemy("Lips",       1,      20)
    };

    /*
    string name;
    int type;
    int maxHealth;
    int currentHealth;
    int block;
    int cooldown;

    EnemyMoves.Move currentMove = new EnemyMoves.Move();

    private static System.Random rand = new System.Random();

    public void TakeDamage(Crd c)
    {
        if (block - c.power <= 0) currentHealth -= block + c.power;
        else block -= c.power;
    }

    public void ChooseMove()
    {
        int i = rand.Next(3);
        currentMove = EnemyMoves.instance.moveList[i];
        cooldown += currentMove.cost;
    }

    public void TakeAction()
    {
        //execute move
        switch (currentMove.type)
        {
            case 0: // if attack
                Player.instance.TakeDamage(currentMove.power); // *** change instance to the actual player 
                break;
            case 1: // if block
                block += currentMove.power;
                break;
            case 2: // if heal
                if (currentHealth + currentMove.power > maxHealth) currentHealth = maxHealth;
                else currentHealth += currentMove.power;
                break;
        }

        //choose move
        ChooseMove();

    }
    
    public void die()
    {
        Destroy(this);
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        //YeetLol.YEET();
        //Debug.Log(YeetLol.heal.name);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (cooldown == 0)
        {
            TakeAction();
        }

        if (currentHealth >= 0) die();
        */
    }
}
