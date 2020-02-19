using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoves : MonoBehaviour
{
    public static EnemyMoves instance;

    void Start()
    {
        instance = this;
    }

    public class Move
    {
        public string name;
        public int type;
        public int power;
        public int cost;

        public Move()
        {
            name = "yeet";
            type = 0;
            power = 1;
            cost = 1;
        }

        public Move(string n, int t, int p, int c)
        {
            name = n;
            type = t;
            power = p;
            cost = c;
        }

    }

    public Move[] moveList = new Move[] 
    {
        //        Name      Type    Power   Cost
        new Move("Punch",   3,      0,      3),
        new Move("Block",   1,      1,      1),
        new Move("Heal",    2,      2,      2)
    };

    /*
    public Move punch = new Move("Punch", 3, 0, 3);
    public Move defend = new Move("Block", 1, 1, 1);
    public Move heal = new Move("Heal", 2, 2, 2);
    public Move yeet = new Move("Yeet", 10, 0, 6);
    public Move eggblast = new Move("Egg Blast", 9001, 0, 2);
    public Move dab = new Move("Dab", 10, 2, 1);
    public Move naynay = new Move("Nay Nay", 20, 1, 10);
    public Move eggcite = new Move("Eggcite", 10000, 2, 6);
    */

    public void YEET()
    {
        Debug.Log("YEET");
        
    }

};
