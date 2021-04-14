using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBase : ScriptableObject
{

    #region Protected Fields

    protected GridTile[,] grid;
    protected Laser[] mapLasers;
    //protected static Vector3 offset = new Vector3(.5f, .5f, 0f);
    protected static Vector3 offset = GridManager.v3TowerOffset;

    [SerializeField]
    protected Material[] materials;
    [SerializeField]
    protected Tower[] mapGenerationTowers;
    [SerializeField]
    protected Vector2Int[] playerStartPositions = new Vector2Int[2];


    #endregion

    #region Public Methods

    public virtual GridTile[,] GenerateMap(int sizeX,int sizeY, Transform holder)
    {
        return grid;
    }

    public virtual Laser[] GetMapLasers()
    {
        return mapLasers;
    }

    public static Vector3 GetOffset()
    {
        return offset;
    }

    public Vector2Int[] GetPlayerStartPositions()
    {
        return playerStartPositions;
    }
    #endregion

    
}
