using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerHit
{
    NULL,
    PLAYER1,
    PLAYER2
}

public struct LaserPoint {
    public Vector3 direction;
    public Vector3 position;
    public Vector3 hitPoint;
    public Vector3 hitNormal;
    public GameObject hitTower;
}

public class Laser : MonoBehaviour
{
    [SerializeField]
    private int maxLaserReflections = 50;
    [SerializeField]
    private Vector3 startDirection;
    LaserPoint startLaserPoint;
    LineRenderer lineRenderer;
    private LayerMask reflectLayer;
    private LayerMask refractLayer;
    private List<LaserPoint> laser = new List<LaserPoint>();

    private PlayerHit hitPlayer = PlayerHit.NULL;

    void Start()
    {
        SettupLaser();
        UpdateLaser();
    }
    //private void Update()
    //{
    //    UpdateLaser();
    //}

    public void UpdateLaser()
    {
        laser.Clear();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startLaserPoint.position);
        RaycastHit hit;
        LaserPoint previousLaserPoint;
        //Debug.DrawRay(startLaserPoint.position, startLaserPoint.direction, Color.red);
        laser.Add(startLaserPoint);

        //TODO
        //DETECT HIT WITH RAYCAST
        //GET BEHAVIOUR OF HIT
        //PROCESS BEHAVIOUR
        //CHECK IF NEW POINT WAS ADDED TO LASER
        //REPEAT

        if (Physics.Raycast(startLaserPoint.position, startLaserPoint.direction, out hit, Mathf.Infinity))
        {
            lineRenderer.positionCount = 2;
            startLaserPoint.hitPoint = hit.point;
            startLaserPoint.hitNormal = hit.normal;
            startLaserPoint.hitTower = hit.transform.gameObject;
            laser.Clear();
            laser.Add(startLaserPoint);
            lineRenderer.SetPosition(1, startLaserPoint.hitPoint);
            previousLaserPoint = startLaserPoint;
            if (hit.transform.gameObject)
            {
                GridTile hitTile = GameManager.instance.GetGridManager().GetGridTileFromGameObject(hit.transform.gameObject);
                if (hitTile.position != Vector2.zero)
                {
                    if (GameManager.instance.GetGridManager().GetTowerFromGameObject(hit.transform.gameObject).GetBehaviour())
                    {
                        GameManager.instance.GetGridManager().GetTowerFromGameObject(hit.transform.gameObject).GetBehaviour().OnLaserHit(this, hitTile);
                    }
                }
            }
            for (int i = 0; i < maxLaserReflections; i++)
            {
                CalculateNewLaserPoint();
                
                if (laser[laser.Count - 1].position != laser[laser.Count - 2].position)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(i + 2, laser[laser.Count - 1].position);
                }
                else
                {
                    break;
                }
            }
        }
    }

    void CalculateNewLaserPoint()
    {
        LaserPoint outLaserPoint = new LaserPoint();
        RaycastHit hit;
        if (Physics.Raycast(laser[laser.Count - 1].position, laser[laser.Count - 1].direction, out hit, Mathf.Infinity))
        {
            GridTile hitTile = GameManager.instance.GetGridManager().GetGridTileFromGameObject(hit.transform.gameObject);
            if (hitTile.currentTowerMesh)
            {
                if (GameManager.instance.GetGridManager().GetTowerFromGameObject(hit.transform.gameObject))
                {

                    if (GameManager.instance.GetGridManager().GetTowerFromGameObject(hit.transform.gameObject).GetBehaviour() != null)
                    {

                        GameManager.instance.GetGridManager().GetTowerFromGameObject(hit.transform.gameObject).GetBehaviour().OnLaserHit(this, hitTile);
                    }
                    else
                    {
                        outLaserPoint.position = hit.point;
                        laser.Add(outLaserPoint);
                    }
                }
                else
                {
                    outLaserPoint.position = hit.point;
                    laser.Add(outLaserPoint);
                }
            }
            if (GameManager.instance.GetGridManager().HitPlayer(hit.transform.parent.gameObject))
            {
                hitPlayer = GameManager.instance.GetGridManager().GetHitPlayer(hit.transform.parent.gameObject);
                GameManager.instance.laserHits.Add(hitPlayer);
            }
        }
    }
    public List<LaserPoint> GetLaser()
    {
        return laser;
    }
    public void SettupLaser()
    {
        startLaserPoint.position = transform.position;
        startLaserPoint.direction = startDirection;
        lineRenderer = GetComponent<LineRenderer>();
        reflectLayer = LayerMask.NameToLayer("Reflect");
        refractLayer = LayerMask.NameToLayer("Refract");
        GameManager.instance.AddLaser(this);
    }
    public void SetStartDirection(Vector3 dir)
    {
        startDirection = dir;
    }

    public PlayerHit HitPlayer()
    {
        return hitPlayer;
    }
}
