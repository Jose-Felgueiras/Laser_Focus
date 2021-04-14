using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

    public Color debugColor = Color.white;
    [SerializeField]
    private int maxLaserReflections = 50;
    [SerializeField]
    private Vector2 startDirection;
    [SerializeField]
    private Vector2 startLaserPoint;
    [SerializeField]
    private Vector2 startDDAPoint;
    LineRenderer lineRenderer;
    private LayerMask reflectLayer;
    private LayerMask refractLayer;
    private List<LaserPoint> laser = new List<LaserPoint>();

    private PlayerHit hitPlayer = PlayerHit.NULL;

    private bool checkFirstCell = true;

    void Start()
    {
        SettupLaser();
        lineRenderer.positionCount = 2;
        DDA(checkFirstCell);
    }


    public void DDA()
    {

        lineRenderer.SetPosition(0, new Vector3(startLaserPoint.x, startLaserPoint.y, -1));

        Vector2 vRayStart = startDDAPoint;

        Vector2 vRayDir = startDirection.normalized;
        Vector2 vRayUnitStepSize = new Vector2(Mathf.Sqrt(1 + (vRayDir.y / vRayDir.x) * (vRayDir.y / vRayDir.x)), Mathf.Sqrt(1 + (vRayDir.x / vRayDir.y) * (vRayDir.x / vRayDir.y)));

        Vector2Int vMapCheck = new Vector2Int((int)(vRayStart.x + GridManager.v3TowerOffset.x), (int)(vRayStart.y + GridManager.v3TowerOffset.y));
        Vector2 vRayLength1D;
        Vector2Int vStep = new Vector2Int();
        Vector2Int vPrevMapCheck = vMapCheck;

        if (vRayDir.x < 0)
        {
            vStep.x = -1;
            vRayLength1D.x = (vRayStart.x - (float)vMapCheck.x) * vRayUnitStepSize.x;
        }
        else
        {
            vStep.x = 1;
            vRayLength1D.x = ((float)(vMapCheck.x + 1) - vRayStart.x) * vRayUnitStepSize.x;

        }

        if (vRayDir.y < 0)
        {
            vStep.y = -1;
            vRayLength1D.y = (vRayStart.y - (float)vMapCheck.y) * vRayUnitStepSize.y;
        }
        else
        {
            vStep.y = 1;
            vRayLength1D.y = ((float)(vMapCheck.y + 1) - vRayStart.y) * vRayUnitStepSize.y;

        }

        bool bTileFound = false;
        float fMaxDistance = 10000.0f;
        float fDistance = 0.0f;
        while (!bTileFound && fDistance < fMaxDistance)
        {
            if (debugColor != Color.white)
            {
                GameManager.instance.GetGridManager().GetGridTile(vPrevMapCheck).background.GetComponent<MeshRenderer>().material.color = debugColor;
                GameManager.instance.GetGridManager().GetGridTile(vMapCheck).background.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                GameManager.instance.GetGridManager().GetGridTile(vPrevMapCheck).background.GetComponent<MeshRenderer>().material.color = debugColor;
                GameManager.instance.GetGridManager().GetGridTile(vMapCheck).background.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            if (vMapCheck.x >= 0 && vMapCheck.x < GameManager.instance.GetGridManager().GetGridDimensions().x && vMapCheck.y >= 0 && vMapCheck.y < GameManager.instance.GetGridManager().GetGridDimensions().y)
            {
                if (!GameManager.instance.GetGridManager().IsTileAvailable(vMapCheck.x, vMapCheck.y))
                {
                    bTileFound = true;
                }
            }

            Vector2 vIntersection;
            if (bTileFound)
            {
                vIntersection = vRayStart + vRayDir * fDistance;
                HitSide hitSide = GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y).FindHitSide(vMapCheck, vPrevMapCheck);
                ELocalHitSide localHitSide = GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y).FindLocalHitSide(hitSide, vMapCheck);

                switch (hitSide)
                {
                    case HitSide.TOP:
                        lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.up * 0.5f);
                        break;
                    case HitSide.RIGHT:
                        lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.right * 0.5f);
                        break;
                    case HitSide.BOTTOM:
                        lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.down * 0.5f);
                        break;
                    case HitSide.LEFT:
                        lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.left * 0.5f);
                        break;
                    default:
                        break;
                }
                if (GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y))
                {
                    if (GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform != transform.parent)
                    {
                        if (GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y).GetBehaviours().Length > 0)
                        {
                            foreach (TowerBehaviour behaviour in GameManager.instance.GetGridManager().GetGridTile(vMapCheck.x, vMapCheck.y).currentTowerMesh.GetComponents<TowerBehaviour>())
                            {
                                behaviour.SetInLaser(this);
                                behaviour.SetHitSide(hitSide);
                                behaviour.SetLocalHitSide(localHitSide);
                                behaviour.OnLaserHit(vRayDir, vMapCheck);
                            }
                        }
                    }
                    else
                    {
                        bTileFound = false;
                    }
                }
            }
            vPrevMapCheck = vMapCheck;
            if (vRayLength1D.x < vRayLength1D.y)
            {
                vMapCheck.x += vStep.x;
                fDistance = vRayLength1D.x;
                vRayLength1D.x += vRayUnitStepSize.x;
            }
            else
            {
                vMapCheck.y += vStep.y;
                fDistance = vRayLength1D.y;
                vRayLength1D.y += vRayUnitStepSize.y;

            }
        }
    }
    public void DDA(bool checkFirstCell)
    {
        if (checkFirstCell)
        {
            lineRenderer.SetPosition(0, new Vector3(startLaserPoint.x, startLaserPoint.y, -1));

            Vector2 vRayStart = startDDAPoint;

            Vector2 vRayDir = startDirection.normalized;
            Vector2 vRayUnitStepSize = new Vector2(Mathf.Sqrt(1 + (vRayDir.y / vRayDir.x) * (vRayDir.y / vRayDir.x)), Mathf.Sqrt(1 + (vRayDir.x / vRayDir.y) * (vRayDir.x / vRayDir.y)));

            Vector2Int vMapCheck = new Vector2Int((int)(vRayStart.x + GridManager.v3TowerOffset.x), (int)(vRayStart.y + GridManager.v3TowerOffset.y));
            Vector2 vRayLength1D;
            Vector2Int vStep = new Vector2Int();
            Vector2Int vPrevMapCheck = new Vector2Int((int)transform.parent.transform.position.x, (int)transform.parent.transform.position.y);

            if (vRayDir.x < 0)
            {
                vStep.x = -1;
                vRayLength1D.x = (vRayStart.x - (float)vMapCheck.x) * vRayUnitStepSize.x;
            }
            else
            {
                vStep.x = 1;
                vRayLength1D.x = ((float)(vMapCheck.x + 1) - vRayStart.x) * vRayUnitStepSize.x;

            }

            if (vRayDir.y < 0)
            {
                vStep.y = -1;
                vRayLength1D.y = (vRayStart.y - (float)vMapCheck.y) * vRayUnitStepSize.y;
            }
            else
            {
                vStep.y = 1;
                vRayLength1D.y = ((float)(vMapCheck.y + 1) - vRayStart.y) * vRayUnitStepSize.y;

            }

            bool bTileFound = false;
            float fMaxDistance = 10000.0f;
            float fDistance = 0.0f;
            while (!bTileFound && fDistance < fMaxDistance)
            {
                if (debugColor != Color.white)
                {
                    GameManager.instance.GetGridManager().GetGridTile(vPrevMapCheck).background.GetComponent<MeshRenderer>().material.color = debugColor;
                    GameManager.instance.GetGridManager().GetGridTile(vMapCheck).background.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                else
                {
                    if ((vMapCheck.x + vMapCheck.y) % 2 == 0)
                    {
                        GameManager.instance.GetGridManager().GetGridTile(vMapCheck).background.GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                    else
                    {
                        GameManager.instance.GetGridManager().GetGridTile(vMapCheck).background.GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                }
                
                if (vMapCheck.x >= 0 && vMapCheck.x < GameManager.instance.GetGridManager().GetGridDimensions().x && vMapCheck.y >= 0 && vMapCheck.y < GameManager.instance.GetGridManager().GetGridDimensions().y)
                {
                    if (!GameManager.instance.GetGridManager().IsTileAvailable(vMapCheck.x, vMapCheck.y))
                    {
                        bTileFound = true;
                    }
                }

                Vector2 vIntersection;
                if (bTileFound)
                {
                    vIntersection = vRayStart + vRayDir * fDistance;
                    HitSide hitSide = GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y).FindHitSide(vMapCheck, vPrevMapCheck);
                    ELocalHitSide localHitSide = GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y).FindLocalHitSide(hitSide, vMapCheck);
                    switch (hitSide)
                    {
                        case HitSide.TOP:
                            lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.up * 0.5f);
                            break;
                        case HitSide.RIGHT:
                            lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.right * 0.5f);
                            break;
                        case HitSide.BOTTOM:
                            lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.down * 0.5f);
                            break;
                        case HitSide.LEFT:
                            lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.left * 0.5f);
                            break;
                        default:
                            break;
                    }

                    if (GameManager.instance.GetGridManager().HitPlayer(vMapCheck))
                    {
                        hitPlayer = GameManager.instance.GetGridManager().GetHitPlayer(vMapCheck);
                        GameManager.instance.laserHits.Add(hitPlayer);
                    }

                    if (GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y))
                    {
                        if (GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform != transform.parent)
                        {
                            if (GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y).GetBehaviours().Length > 0)
                            {
                                foreach (TowerBehaviour behaviour in GameManager.instance.GetGridManager().GetGridTile(vMapCheck.x, vMapCheck.y).currentTowerMesh.GetComponents<TowerBehaviour>())
                                {
                                    behaviour.SetInLaser(this);
                                    behaviour.SetHitSide(hitSide);
                                    behaviour.SetLocalHitSide(localHitSide);
                                    behaviour.OnLaserHit(vRayDir, vMapCheck);
                                }
                            }
                        }
                        else
                        {
                            bTileFound = false;
                        }
                    }
                }
                vPrevMapCheck = vMapCheck;
                if (vRayLength1D.x < vRayLength1D.y)
                {
                    vMapCheck.x += vStep.x;
                    fDistance = vRayLength1D.x;
                    vRayLength1D.x += vRayUnitStepSize.x;
                }
                else
                {
                    vMapCheck.y += vStep.y;
                    fDistance = vRayLength1D.y;
                    vRayLength1D.y += vRayUnitStepSize.y;

                }
            }
        }
        else
        {

            lineRenderer.SetPosition(0, new Vector3(startLaserPoint.x, startLaserPoint.y, -1));

            Vector2 vRayStart = startDDAPoint;

            Vector2 vRayDir = startDirection.normalized;
            Vector2 vRayUnitStepSize = new Vector2(Mathf.Sqrt(1 + (vRayDir.y / vRayDir.x) * (vRayDir.y / vRayDir.x)), Mathf.Sqrt(1 + (vRayDir.x / vRayDir.y) * (vRayDir.x / vRayDir.y)));

            Vector2Int vMapCheck = new Vector2Int((int)(vRayStart.x + GridManager.v3TowerOffset.x), (int)(vRayStart.y + GridManager.v3TowerOffset.y));
            Vector2 vRayLength1D;
            Vector2Int vStep = new Vector2Int();
            Vector2Int vPrevMapCheck = new Vector2Int((int)transform.parent.transform.position.x, (int)transform.parent.transform.position.y);

            if (vRayDir.x < 0)
            {
                vStep.x = -1;
                vRayLength1D.x = (vRayStart.x - (float)vMapCheck.x) * vRayUnitStepSize.x;
            }
            else
            {
                vStep.x = 1;
                vRayLength1D.x = ((float)(vMapCheck.x + 1) - vRayStart.x) * vRayUnitStepSize.x;

            }

            if (vRayDir.y < 0)
            {
                vStep.y = -1;
                vRayLength1D.y = (vRayStart.y - (float)vMapCheck.y) * vRayUnitStepSize.y;
            }
            else
            {
                vStep.y = 1;
                vRayLength1D.y = ((float)(vMapCheck.y + 1) - vRayStart.y) * vRayUnitStepSize.y;

            }

            bool bTileFound = false;
            float fMaxDistance = 10000.0f;
            float fDistance = 0.0f;
            int i = 0;
            while (!bTileFound && fDistance < fMaxDistance)
            {
                if (i != 0)
                {
                    vPrevMapCheck = vMapCheck;
                }

                if (vRayLength1D.x < vRayLength1D.y)
                {
                    vMapCheck.x += vStep.x;
                    fDistance = vRayLength1D.x;
                    vRayLength1D.x += vRayUnitStepSize.x;
                }
                else
                {
                    vMapCheck.y += vStep.y;
                    fDistance = vRayLength1D.y;
                    vRayLength1D.y += vRayUnitStepSize.y;

                }
                if (debugColor != Color.white)
                {
                    GameManager.instance.GetGridManager().GetGridTile(vPrevMapCheck).background.GetComponent<MeshRenderer>().material.color = debugColor;
                    GameManager.instance.GetGridManager().GetGridTile(vMapCheck).background.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                else
                {
                    if ((vMapCheck.x + vMapCheck.y) % 2 == 0)
                    {
                        GameManager.instance.GetGridManager().GetGridTile(vMapCheck).background.GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                    else
                    {
                        GameManager.instance.GetGridManager().GetGridTile(vMapCheck).background.GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                }
                if (vMapCheck.x >= 0 && vMapCheck.x < GameManager.instance.GetGridManager().GetGridDimensions().x && vMapCheck.y >= 0 && vMapCheck.y < GameManager.instance.GetGridManager().GetGridDimensions().y)
                {
                    if (!GameManager.instance.GetGridManager().IsTileAvailable(vMapCheck.x, vMapCheck.y))
                    {
                        bTileFound = true;
                    }
                }

                Vector2 vIntersection;
                if (bTileFound)
                {
                    vIntersection = vRayStart + vRayDir * fDistance;
                    HitSide hitSide = GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y).FindHitSide(vMapCheck, vPrevMapCheck);
                    ELocalHitSide localHitSide = GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y).FindLocalHitSide(hitSide, vMapCheck);

                    switch (hitSide)
                    {
                        case HitSide.TOP:
                            lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.up * 0.5f);
                            break;
                        case HitSide.RIGHT:
                            lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.right * 0.5f);
                            break;
                        case HitSide.BOTTOM:
                            lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.down * 0.5f);
                            break;
                        case HitSide.LEFT:
                            lineRenderer.SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform.position + Vector3.left * 0.5f);
                            break;
                        default:
                            break;
                    }

                    if (GameManager.instance.GetGridManager().HitPlayer(vMapCheck))
                    {
                        hitPlayer = GameManager.instance.GetGridManager().GetHitPlayer(vMapCheck);
                        GameManager.instance.laserHits.Add(hitPlayer);
                    }
                    if (GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y))
                    {
                        if (GameManager.instance.GetGridManager().GetGridTile(vMapCheck).currentTowerMesh.transform != transform.parent)
                        {
                            if (GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y))
                            {
                                if (GameManager.instance.GetGridManager().GetTower(vMapCheck.x, vMapCheck.y).GetBehaviours().Length > 0)
                                {
                                    foreach (TowerBehaviour behaviour in GameManager.instance.GetGridManager().GetGridTile(vMapCheck.x, vMapCheck.y).currentTowerMesh.GetComponents<TowerBehaviour>())
                                    {
                                        behaviour.SetInLaser(this);
                                        behaviour.SetHitSide(hitSide);
                                        behaviour.SetLocalHitSide(localHitSide);
                                        behaviour.OnLaserHit(vRayDir, vMapCheck);
                                    }
                                }
                            }
                        }
                        else
                        {
                            bTileFound = false;
                        }
                    }
                }
                i++;
            }
        }
       
    }

    public List<LaserPoint> GetLaser()
    {
        return laser;
    }
    public void SettupLaser()
    {
        lineRenderer = GetComponent<LineRenderer>();
        reflectLayer = LayerMask.NameToLayer("Reflect");
        refractLayer = LayerMask.NameToLayer("Refract");
        GameManager.instance.AddLaser(this.gameObject);
    }
    public void SetStartDirection(Vector2 dir)
    {
        startDirection = dir;
    }
    public void SetStartPosition(Vector2 pos)
    {
        startLaserPoint = pos;
    }
    public void SetStartDDAPoint(Vector2 pos)
    {
        startDDAPoint = pos;
    }
    public void SetCheckFirstCell(bool value)
    {
        checkFirstCell = value;
    }
    public PlayerHit HitPlayer()
    {
        return hitPlayer;
    }
}
