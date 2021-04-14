using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    [SerializeField]
    private int[] deck = new int[8];

    public void SettupDeck(int[] _deck)
    {

        deck = _deck;
    }

    public Tower GetTower(int index)
    {
        return AllTowers.instance.GetTowerFromIndex(deck[index]);
    }

    public int GetIndex(int index)
    {
        return deck[index];
    }

}
