using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crd : MonoBehaviour
{
    public string cardName = "A Card";
    // * variable for the art here *
    public int type = 1;
    public int power = 5;
    public int cost = 1;
    public GameObject CrdText;
    public GameObject battlemanager;
    Vector3 startpos;
    public enum CardState
    {
        isFocus,
        notFocus,
    }
    CardState state;
    void Start()
    {
        state = CardState.notFocus;
        CrdText.GetComponent<TextMesh>().text = cardName;
        startpos = gameObject.transform.position;

    }
  //  public bool isCardTheFocus()
   // {

   //     return true;
 //   }
    void Update()
    {
      
      
    }
    public void changeName(string newName)
    {
        name = newName;
        cardName = newName;
        CrdText.GetComponent<TextMesh>().text = cardName;
    }
    public string description()
    {
        switch (type)
        {
            case 0:
                return "Deal " + power + " Damage";
            case 1:
                return "Gain " + power + " Block";
            case 2:
                return "Restore " + power + " HP";
        }
        return "Deal " + power + " Damage";

    }

    public void TriggerEffect()
    {
        switch (type)
        {
            case 1: // gain block
                Player.instance.GainBlock(power);
                break;
            case 2: // heal
                Player.instance.Heal(power);
                break;
        }
        Player.instance.PlayCard(this);
    }

    public void TriggerEffect(EnemyScript.Enemy target)
    {
        target.TakeDamage(this);
        Player.instance.PlayCard(this);
    }
    public void OnClick()
    {
        // int numcrds;
        //Removes focus from whatever card was focused on before
        Debug.Log("Clicked on card");
        if (state != CardState.isFocus)
        {


            GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<Hand>().getFocus();// Seems to actually work to remove the bool;

            GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<Hand>().focusOnCard(gameObject);
            state = CardState.isFocus;
            Vector3 newpos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y +200, gameObject.transform.position.z - 10f);
            gameObject.transform.position = newpos;
        }
        else if (state == CardState.isFocus)
        {
            GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>().PlayerMove(type);
            GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<Hand>().useCard(gameObject);
            
        }
    }
    public void OnMouseDown()
    {  // int numcrds;
        //Removes focus from whatever card was focused on before
        Debug.Log("Clicked on card");
        if (state != CardState.isFocus)
        {

      
        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<Hand>().getFocus();// Seems to actually work to remove the bool;
        
        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<Hand>().focusOnCard(gameObject);
        state = CardState.isFocus;
        Vector3 newpos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 15, gameObject.transform.position.z - 0.2f);
        gameObject.transform.position = newpos;
        } 
        else if(state == CardState.isFocus) {

            GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<Hand>().useCard(gameObject);

        }
    }
    public void RemoveFocus()
    {
        state = CardState.notFocus;
        gameObject.transform.position = startpos;
    }
    public void changeStandardPos(Vector3 newpos)
    {
        startpos = newpos;

    }
}
    