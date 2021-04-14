using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerID
{
    LOCALPLAYER,
    PLAYER
}

public enum Winner
{
    NONE,
    TIE,
    PLAYER1,
    PLAYER2
}

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

   
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public Material localPlayerColor;
    public Material playerColor;

    public static PlayerID player;

    public List<PlayerHit> laserHits { get; private set; } = new List<PlayerHit>();

    private Winner winner;

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        //GameObject _player;
        //if (_id == Client.instance.id)
        //{
        //    _player = Instantiate(localPlayerPrefab, _position, _rotation);
        //}
        //else
        //{
        //    _player = Instantiate(playerPrefab, _position, _rotation);
        //}

        //_player.GetComponent<PlayerManager>().id = _id;
        //_player.GetComponent<PlayerManager>().username = _username;
        //players.Add(_id, _player.GetComponent<PlayerManager>());

    }


    //END OF TESTING


    [SerializeField]
    private PlayerDeck playerDeck;
    [SerializeField]
    private InGameHUD inGameHUD;
    [SerializeField]
    private PlayerController playerController;

    private GridManager gridManager;

    private List<GameObject> lasers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //TODO
        //CREATE GRID MANAGER ON START
        //CREATE REFERENCE

        //GameObject grid = new GameObject("Grid Manager");
        //grid.AddComponent<GridManager>();

        gridManager = GameObject.FindObjectOfType<GridManager>().GetComponent<GridManager>();
        gridManager.GenerateGrid();

        ClientSend.RequestPlayer();
        ClientSend.PlayerSuccessfullyLoadedGame();
        //Camera.main.transform.position = new Vector3((int)(gridManager.GetGridDimensions().x / 2), gridManager.GetGridDimensions().x + gridManager.GetGridDimensions().y, (int)(gridManager.GetGridDimensions().y / 2));
    }

    public PlayerDeck GetPlayerDeck()
    {
        return playerDeck;
    }

    public GridManager GetGridManager()
    {
        return gridManager;
    }

    public void AddLaser(GameObject newLaser)
    {
        lasers.Add(newLaser);
    }
    public void ClearLasers()
    {
        if (lasers.Count > 0)
        {
            GameObject starter = lasers[0];
            lasers.Remove(starter);
            if (lasers.Count > 0)
            {
                foreach (GameObject laser in lasers.ToArray())
                {
                    Destroy(laser);
                }
            }
            lasers.Clear();
            lasers.Add(starter);
        }
    }
    public void UpdateLasers()
    {
        ClearLasers();
        lasers[0].GetComponent<Laser>().DDA(false);
        //if (lasers.Count > 0)
        //{
        //    foreach (GameObject laser in lasers.ToArray())
        //    {
        //        if(laser.GetComponent<Laser>())
        //            laser.GetComponent<Laser>().DDA(false);
        //    }
        //}
    }

    public PlayerController GetLocalPlayerController()
    {
        return playerController;
    }

    public void PlaceTower(int _player, Vector2 _pos, Quaternion _rot, int _towerIndex)
    {
        if (gridManager.IsTileAvailable(_pos))
        {
            GameObject tower = Instantiate(AllTowers.instance.GetTowerFromIndex(_towerIndex).GetGameObject(), new Vector3(_pos.x, _pos.y, -1) + MapBase.GetOffset(), _rot);

            tower.transform.SetParent(gridManager.GetGridTile(_pos).background.transform);
            gridManager.SetGridTileTower(_pos, tower, AllTowers.instance.GetTowerFromIndex(_towerIndex));

            if (AllTowers.instance.GetTowerFromIndex(_towerIndex).GetBehaviours().Length > 0)
            {
                foreach (TowerBehaviour behaviour in AllTowers.instance.GetTowerFromIndex(_towerIndex).GetBehaviours())
                {
                    TowerBehaviour newBehaviour = tower.AddComponent(behaviour.GetType()) as TowerBehaviour;
                    newBehaviour.SetGridTile(gridManager.GetGridTile(_pos));
                    newBehaviour.OnPlace(_pos);
                }
            }
            if (_player == PlayerConfig.GetPlayerID())
            {
                //BLUE
                tower.GetComponent<MeshRenderer>().material = localPlayerColor;
                gridManager.SetTileOwner(_pos, TileOwner.PLAYER1);
            }
            else
            {
                //RED
                tower.GetComponent<MeshRenderer>().material = playerColor;
                gridManager.SetTileOwner(_pos, TileOwner.PLAYER2);

            }
        }
        gridManager.ClearLaserHits();
        UpdateLasers();


        //Weird bug, needs delay to find winner
        StartCoroutine(CheckWinnerDelay());

    }

    public void SetPlayer(int _number)
    {
        player = (PlayerID)_number;

        if (player == PlayerID.LOCALPLAYER)
        {
            gridManager.GetGridTile(gridManager.GetMap().GetPlayerStartPositions()[0]).currentTowerMesh.GetComponent<MeshRenderer>().material = localPlayerColor;
            gridManager.GetGridTile(gridManager.GetMap().GetPlayerStartPositions()[1]).currentTowerMesh.GetComponent<MeshRenderer>().material = playerColor;
            //gridManager.player1.GetComponentInChildren<MeshRenderer>().material = localPlayerColor;
            //gridManager.player2.GetComponentInChildren<MeshRenderer>().material = playerColor;
        }
        else
        {
            //gridManager.player1.GetComponentInChildren<MeshRenderer>().material = playerColor;
            //gridManager.player2.GetComponentInChildren<MeshRenderer>().material = localPlayerColor;
            gridManager.GetGridTile(gridManager.GetMap().GetPlayerStartPositions()[0]).currentTowerMesh.GetComponent<MeshRenderer>().material = playerColor;
            gridManager.GetGridTile(gridManager.GetMap().GetPlayerStartPositions()[1]).currentTowerMesh.GetComponent<MeshRenderer>().material = localPlayerColor;
        }
    }

    public Winner CheckForWinners()
    {
        bool hitPlayer1 = false;
        bool hitPlayer2 = false;
        //for (int i = 0; i < laserHits.Count; i++)
        //{
        //    Debug.Log("BBB");

        //    Debug.Log(laserHits[i]);
        //    if (laserHits[i] == PlayerHit.PLAYER1)
        //    {
        //        hitPlayer1 = true;
        //    }
        //    if (laserHits[i] == PlayerHit.PLAYER2)
        //    {
        //        hitPlayer2 = true;
        //    }
        //}
        foreach (var laser in laserHits.ToArray())
        {
            if (laser == PlayerHit.PLAYER1)
            {
                hitPlayer1 = true;
            }
            if (laser == PlayerHit.PLAYER2)
            {
                hitPlayer2 = true;
            }
        }

        if (hitPlayer1 && hitPlayer2)
        {
            return Winner.TIE;
        }
        if (hitPlayer1)
        {
            if (player == PlayerID.LOCALPLAYER)
            {
                return Winner.PLAYER2;
            }
            else
            {
                return Winner.PLAYER1;
            }
        }
        if (hitPlayer2)
        {
            if (player == PlayerID.LOCALPLAYER)
            {
                return Winner.PLAYER1;
            }
            else
            {
                return Winner.PLAYER2;
            }
        }
        return Winner.NONE;
    }

    IEnumerator CheckWinnerDelay()
    {
        yield return new WaitForSeconds(1.0f);

        winner = CheckForWinners();
        if (winner != Winner.NONE)
        {
            inGameHUD.ShowGameOverPanel(winner);
            if (winner != Winner.TIE)
            {
                if (player == PlayerID.LOCALPLAYER)
                {
                    if (winner == Winner.PLAYER1)
                    {
                        ClientSend.MatchWinner(0);
                    }
                    if (winner == Winner.PLAYER2)
                    {
                        ClientSend.MatchWinner(1);
                    }
                }
            }
            else
            {
                ClientSend.MatchWinner(2);
            }
        }
    }
}
