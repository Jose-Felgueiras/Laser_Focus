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

    public int[] GetDecksIndices()
    {
        int[] values = new int[deck.Length];
        for (int i = 0; i < values.Length; i++)
        {

            values[i] = AllTowers.instance.GetTowerIndex(deck[i]);
            if (values[i] < 0)
            {
                values[i] = 0;
            }
        }
        return values;
    }

    public void SetDeck(int index)
    {
        int[] values = new int[deck.Length];
        values = PlayerConfig.GetDeck(index);
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = AllTowers.instance.GetTowerFromIndex(values[i]);
        }
    }
}
