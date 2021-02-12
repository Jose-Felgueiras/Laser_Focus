using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTowers : MonoBehaviour
{
    public static AllTowers instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField]
    private List<Tower> allTowers;


    public Tower GetTowerFromIndex(int _index)
    {
        return allTowers[_index];
    }

    public int GetTowerCount()
    {
        return allTowers.Count;
    }

    public int GetTowerIndex(Tower _tower)
    {
        return allTowers.IndexOf(_tower);
    }
}
