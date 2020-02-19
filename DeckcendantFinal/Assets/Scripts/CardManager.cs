using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The card manager is used to handle interactions between Deck classes, the hand, and the battlehandler
public class CardManager : MonoBehaviour
{
     GameObject deck;
     GameObject hand;
     GameObject drawPile;
     GameObject discardPile;
     GameObject graveyard;

    // Start is called before the first frame update
    void OnAwake(){

    }
    void Start()
    {
        deck = GameObject.FindGameObjectWithTag("PlayerDeck");
        hand = GameObject.FindGameObjectWithTag("PlayerHand");
        drawPile = GameObject.FindGameObjectWithTag("PlayerDrawPile");
        discardPile = GameObject.FindGameObjectWithTag("PlayerDiscardPile");
        graveyard = GameObject.FindGameObjectWithTag("PlayerGraveyard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public int getDeckID()
    {
        return deck.GetInstanceID();
    }
    public int getHandID()
    {
        return hand.GetInstanceID();
    }
    public int getDrawPileID()
    {
        return drawPile.GetInstanceID();
    }
    public int getDiscardPileID()
    {
        return discardPile.GetInstanceID();
    }
    public int getGraveyardID()
    {
        return graveyard.GetInstanceID();
    }
}
