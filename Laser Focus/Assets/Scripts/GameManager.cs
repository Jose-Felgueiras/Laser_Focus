using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

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
        gridManager = GameObject.FindObjectOfType<GridManager>().GetComponent<GridManager>();
        Camera.main.transform.position = new Vector3((int)(gridManager.GetGridDimensions().x / 2), gridManager.GetGridDimensions().x + gridManager.GetGridDimensions().y, (int)(gridManager.GetGridDimensions().y / 2));
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
