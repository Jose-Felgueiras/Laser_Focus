using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int[] deck = new int[8];

    void Start()
    {
        
    }

    void UpdateDeck()
    {
        
    }
    public Tower GetTower(int index)
    {
        return GameManager.instance.towers[deck[index]];
    }

}
