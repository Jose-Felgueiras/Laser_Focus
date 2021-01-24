using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileOwner
{
    GAMEMANAGER, PLAYER1, PLAYER2
}

public struct GridTile
{
    public Vector2 position;
    public GameObject background;
    public GameObject currentTowerMesh;
    public Tower currentTower;
    public TileOwner owner;
}

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField]
    private int gridSizeX;
    [SerializeField]
    private int gridSizeY;


    private GameObject gridHolder;
    private GameObject wallsHolder;



    [SerializeField]
    private MapBase map;



    private GridTile[,] grid;


    // Start is called before the first frame update
    void Start()
    {
        gridHolder = new GameObject("Grid Holder");
        gridHolder.transform.parent = transform;
        wallsHolder = new GameObject("Walls Holder");
        wallsHolder.transform.parent = transform;

        GenerateGrid();
    }

    public void GenerateGrid()
    {

        grid = map.GenerateMap(gridSizeX, gridSizeY, gridHolder.transform);
    }
    public Vector2 GetBackgroundCoordsFromIndex(int index)
    {
        int x, y;
        y = Mathf.FloorToInt((float)index / (float)gridSizeY);
        x = index % gridSizeX;

        return new Vector2(x, y);
    }

    public bool IsBackgroundEmpty(Vector2 gridCoords)
    {
        if (grid[(int)gridCoords.x, (int)gridCoords.y].currentTower)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Tower GetTowerFromCoords(Vector2 gridCoords)
    {
        if (grid[(int)gridCoords.x, (int)gridCoords.y].currentTower)
        {
            return grid[(int)gridCoords.x, (int)gridCoords.y].currentTower;
        }
        else
        {
            return null;
        }
    }
    public Tower GetTowerFromGameObject(GameObject hitTower)
    {
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                if (grid[x,y].currentTowerMesh == hitTower && grid[x,y].currentTower)
                {
                    return grid[x, y].currentTower;
                }
            }
        }
        return null;
    }

    public GridTile GetGridTileFromGameObject(GameObject hitTower)
    {
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                if (grid[x, y].currentTowerMesh == hitTower && grid[x, y].currentTower)
                {
                    return grid[x, y];
                }
            }
        }
        return grid[0, 0];
    }

    public void SetGridTileTower(Vector2 gridCoords, GameObject towerMesh, Tower tower)
    {
        grid[(int)gridCoords.x, (int)gridCoords.y].currentTowerMesh = towerMesh;
        grid[(int)gridCoords.x, (int)gridCoords.y].currentTower = tower;
    }

    public void ClearGridTileTower(Vector2 gridCoords)
    {
        Destroy(grid[(int)gridCoords.x, (int)gridCoords.y].currentTower);
        grid[(int)gridCoords.x, (int)gridCoords.y].currentTower = null;
    }

    public Vector2 GetGridDimensions()
    {
        return new Vector2(gridSizeX, gridSizeY);
    }
}


