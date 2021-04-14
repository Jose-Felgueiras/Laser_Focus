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

    public static Vector3 v3TowerOffset { get; private set; } = new Vector3(.5f, .5f, 0);
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

        //SettupPlayers();
    }
    public Vector2 GetBackgroundCoordsFromIndex(int index)
    {
        int x, y;
        y = Mathf.FloorToInt((float)index / (float)gridSizeX);
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

    public Tower GetTower(int x, int y)
    {
        return grid[x, y].currentTower;
    }

    public Tower GetTower(float x, float y)
    {
        return grid[(int)x, (int)y].currentTower;
    }

    public Tower GetTower(Vector2 pos)
    {
        return grid[(int)pos.x, (int)pos.y].currentTower;
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

    public GridTile GetGridTile(Vector2Int _pos)
    {
        return grid[_pos.x, _pos.y];
    }

    public GridTile GetGridTile(int x, int y)
    {
        return grid[x, y];
    }
    public GridTile GetGridTile(float x, float y)
    {
        return grid[(int)x, (int)y];
    }
    public void SetGridTileTower(Vector2 gridCoords, GameObject towerMesh, Tower tower)
    {
        grid[(int)gridCoords.x, (int)gridCoords.y].currentTowerMesh = towerMesh;
        grid[(int)gridCoords.x, (int)gridCoords.y].currentTower = tower;
    }

    public void ClearGridTileTower(Vector2 gridCoords)
    {
        Destroy(grid[(int)gridCoords.x, (int)gridCoords.y].currentTowerMesh.gameObject);
        grid[(int)gridCoords.x, (int)gridCoords.y].currentTower = null;
        grid[(int)gridCoords.x, (int)gridCoords.y].currentTowerMesh = null;
        grid[(int)gridCoords.x, (int)gridCoords.y].owner = TileOwner.GAMEMANAGER;
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
    public bool IsTileAvailable(int x, int y)
    {
        if (!grid[x, y].currentTower)
        {
            return true;
        }
        return false;
    }
    public void ClearLaserHits()
    {
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                if (grid[x, y].currentTower)
                {
                    foreach (TowerBehaviour behaviour in grid[x, y].currentTowerMesh.GetComponents<TowerBehaviour>())
                    {
                        behaviour.ClearAllHits();
                    }
                }
                if (grid[x, y].background.GetComponent<MeshRenderer>())
                {
                    if ((x + y) % 2 == 0)
                    {
                        grid[x, y].background.GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                    else
                    {
                        grid[x, y].background.GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                }
            }
        }
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

    public bool HitPlayer(Vector2 _pos)
    {
        if ((Vector2)map.GetPlayerStartPositions()[0] == _pos)
        {
            return true;
        }
        if ((Vector2)map.GetPlayerStartPositions()[1] == _pos)
        {
            return true;
        }
        return false;
    }

    public bool HitPlayer(Vector2Int _pos)
    {
        if (map.GetPlayerStartPositions()[0] == _pos)
        {
            return true;
        }
        if (map.GetPlayerStartPositions()[1] == _pos)
        {
            return true;
        }
        return false;
    }

    public bool HitPlayer(GameObject _hitObj)
    {
        return player1 == _hitObj || player2 == _hitObj;
    }

    public PlayerHit GetHitPlayer(Vector2 _pos)
    {
        if ((Vector2)map.GetPlayerStartPositions()[0] == _pos)
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
        if ((Vector2)map.GetPlayerStartPositions()[1] == _pos)
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

    public PlayerHit GetHitPlayer(Vector2Int _pos)
    {
        if (map.GetPlayerStartPositions()[0] == _pos)
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
        if (map.GetPlayerStartPositions()[1] == _pos)
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

    public MapBase GetMap()
    {
        return map;
    }
}


