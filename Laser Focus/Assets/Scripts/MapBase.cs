using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBase : ScriptableObject
{

    #region Protected Fields

    protected GridTile[,] grid;
    [SerializeField]
    protected Material[] materials;
    [SerializeField]
    protected Tower[] mapGenerationTowers;

    #endregion

    #region Public Methods

    public virtual GridTile[,] GenerateMap(int sizeX,int sizeY, Transform holder)
    {
        return grid;
    }

    #endregion

    
}
