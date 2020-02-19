using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private GameObject playerHolder;
    [SerializeField] private GameObject enemyHolder;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy; 

   
    private static BattleHandler instance;
    private State state;

    public static BattleHandler GetInstance()
    {
        return instance;
    }

    private enum State
    {
       WaitingForPlayer,
       Busy,
       StartOfTurn,
       EndOfTurn,
       
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnCombatant(true, player,playerHolder);
        SpawnCombatant(false, enemy, enemyHolder);

        state = State.WaitingForPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.WaitingForPlayer)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
                 {
                    state = State.Busy;
                    Debug.Log("State is busy;");
                 }
        }
        if (state == State.Busy)
        {
            
            state = State.WaitingForPlayer;
            Debug.Log("State is waiting for players");
        }
     
    }
    private void SpawnCombatant(bool isFriendly, GameObject combatant, GameObject holder)
    {
        
       
      /*  if (isFriendly)
        {
            pos = new Vector3(-5.0f, 0.0f, 0.0f);

        } else
        {
            pos = new Vector3(+5.0f, 0.0f, 0.0f);
        }*/
        Instantiate(combatant, holder.transform.position, combatant.transform.rotation);
    }
}
