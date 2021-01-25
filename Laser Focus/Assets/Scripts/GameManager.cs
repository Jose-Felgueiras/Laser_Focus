using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

   

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

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.id)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        players.Add(_id, _player.GetComponent<PlayerManager>());

    }


    //END OF TESTING

    [SerializeField]
    public Tower[] towers;
 

    [SerializeField]
    private PlayerDeck playerDeck;

    GridManager gridManager;

    private List<Laser> lasers = new List<Laser>();

    // Start is called before the first frame update
    void Start()
    {
        //TODO
        //CREATE GRID MANAGER ON START
        //CREATE REFERENCE

        //GameObject grid = new GameObject("Grid Manager");
        //grid.AddComponent<GridManager>();



        //gridManager = GameObject.FindObjectOfType<GridManager>().GetComponent<GridManager>();
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

    public void AddLaser(Laser newLaser)
    {
        lasers.Add(newLaser);
    }
    public void ClearLasers()
    {
        if (lasers.Count > 0)
        {
            Laser starter = lasers[0];
            lasers.Remove(starter);
            if (lasers.Count > 0)
            {
                foreach (Laser laser in lasers.ToArray())
                {
                    Destroy(laser.gameObject);
                }
            }
            lasers.Clear();
            lasers.Add(starter);
        }
    }
    public void UpdateLasers()
    {
        ClearLasers();
        if (lasers.Count > 0)
        {
            foreach (Laser laser in lasers.ToArray())
            {
                laser.UpdateLaser();
            }
        }
    }
}
