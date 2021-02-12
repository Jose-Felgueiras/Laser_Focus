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

    [SerializeField]
    private GameObject playerPrefab;

    public GameObject player1;
    public GameObject player2;

    private GridTile[,] grid;


    //// Start is called before the first frame update
    //void Start()
    //{

    //    wallsHolder = new GameObject("Walls Holder");
    //    wallsHolder.transform.parent = transform;


    //}

    public void GenerateGrid()
    {
        gridHolder = new GameObject("Grid Holder");
        gridHolder.transform.parent = transform;
        grid = map.GenerateMap(gridSizeX, gridSizeY, gridHolder.transform);
        SettupPlayers();
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
        if (grid[(int)gridCoords.x, (int)gridCoords.y].currentTower || grid[(int)gridCoords.x, (int)gridCoords.y].currentTowerMesh)
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

    public GridTile GetGridTile(int _index)
    {
        Vector2 _pos = GetBackgroundCoordsFromIndex(_index);

        return grid[(int)_pos.x, (int)_pos.y];
    }

    public GridTile GetGridTile(Vector2 _pos)
    {
        return grid[(int)_pos.x, (int)_pos.y];
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

    public bool IsTileAvailable(Vector2 pos)
    {
        if (!grid[(int)pos.x, (int)pos.y].currentTower)
        {
            return true;
        }
        return false;
    }

    public void SetTileOwner(Vector2 _pos, TileOwner _owner)
    {
        grid[(int)_pos.x, (int)_pos.y].owner = _owner;
    }

    public void SettupPlayers()
    {
        player1 = Instantiate(playerPrefab);
        grid[3, 3].currentTowerMesh = player1;
        player1.transform.position = new Vector3(grid[3, 3].position.x, .5f, grid[3, 3].position.y);


        player2 = Instantiate(playerPrefab);
        grid[21, 21].currentTowerMesh = player2;
        player2.transform.position = new Vector3(grid[21, 21].position.x, .5f, grid[21, 21].position.y);
    }

    public bool HitPlayer(GameObject _hitObj)
    {
        return player1 == _hitObj || player2 == _hitObj;
    }

    public PlayerHit GetHitPlayer(GameObject _hitObj)
    {
        if (player1 == _hitObj)
        {
            if (GameManager.player == PlayerID.LOCALPLAYER)
            {
                return PlayerHit.PLAYER1;
            }
            else
            {
                return PlayerHit.PLAYER2;
            }
        }
        if (player2 == _hitObj)
        {
            if (GameManager.player == PlayerID.LOCALPLAYER)
            {
                return PlayerHit.PLAYER2;
            }
            else
            {
                return PlayerHit.PLAYER1;
            }
        }
        return PlayerHit.NULL;
    }
}


