using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private Tower[] deck = new Tower[8];

    public Tower[] GetDeck()
    {
        return deck;
    }
    public void SetDeckTower(int index, Tower tower)
    {
        deck[index] = tower;
    }
}
