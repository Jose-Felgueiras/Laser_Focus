using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum HitSide
{
    TOP, RIGHT, BOTTOM, LEFT
}

[CreateAssetMenu(fileName = "Split Behaviour", menuName = "Towers/Tower Behavior/Split")]
public class SplitBehaviour : TowerBehaviour
{
    #region Private Fields

    private LaserPoint outLaserPoint;
    private Vector3[] startPoisitions;

    #endregion

    #region TowerBehaviour Callbacks

    #endregion

    public override void OnLaserHit(Laser hitLaser, GridTile hitTower)
    {

        Vector3[] directions = FindLaserDirections(hitLaser);

        //HitSide hitSide = FindHitSide(hitLaser);

        if (hitTower.currentTowerMesh.transform.childCount < 12)
        {
            GameObject split1 = new GameObject("Laser A");
            split1.transform.parent = hitTower.currentTowerMesh.transform;


            GameObject split2 = new GameObject("Laser B");
            split2.transform.parent = hitTower.currentTowerMesh.transform;

            startPoisitions = FindStartPositions(hitLaser);
            split1.transform.localPosition = startPoisitions[0] * .5f;
            split2.transform.localPosition = startPoisitions[1] * .5f;

            //switch (hitSide)
            //{
            //    case HitSide.TOP:

            //        split1.transform.localPosition = Vector3.back * .5f;
            //        split2.transform.localPosition = Vector3.back * .5f;
            //        break;
            //    case HitSide.RIGHT:
            //        split1.transform.localPosition = Vector3.left * .5f;
            //        split2.transform.localPosition = Vector3.left * .5f;
            //        break;
            //    case HitSide.BOTTOM:
            //        split1.transform.localPosition = Vector3.forward * .5f;
            //        split2.transform.localPosition = Vector3.forward * .5f;
            //        break;
            //    case HitSide.LEFT:
            //        split1.transform.localPosition = Vector3.right * .5f;
            //        split2.transform.localPosition = Vector3.right * .5f;
            //        break;
            //    default:
            //        split1.transform.localPosition = Vector3.zero;
            //        split2.transform.localPosition = Vector3.zero;
            //        break;
            //}
            Debug.DrawRay(split1.transform.position, directions[0], Color.red, 1000.0f);
            Debug.DrawRay(split2.transform.position, directions[1], Color.red, 1000.0f);


            split1.AddComponent<LineRenderer>();
            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
            Laser laser1 = split1.AddComponent<Laser>();
            laser1.SetStartDirection(directions[0]);

            split2.AddComponent<LineRenderer>();
            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
            Laser laser2 = split2.AddComponent<Laser>();
            laser2.SetStartDirection(directions[1]);

            GameManager.instance.AddLaser(laser1);
            GameManager.instance.AddLaser(laser2);
        }


        //CREATE LAST POINT FOR PREVIOUS LASER
        RaycastHit hit;
        outLaserPoint.position = hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].position;
        outLaserPoint.direction = hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].direction;
        if (Physics.Raycast(outLaserPoint.position, outLaserPoint.direction, out hit, Mathf.Infinity))
        {

            outLaserPoint.position = hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].hitPoint;
            outLaserPoint.hitPoint = hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].hitPoint;
            outLaserPoint.direction = Vector3.zero;
            hitLaser.GetLaser().Add(outLaserPoint);
        }
       
    }

    HitSide FindHitSide(Laser laser)
    {
        float vert = Vector3.Dot(Vector3.forward, laser.GetLaser()[laser.GetLaser().Count - 1].hitNormal);
        float hor = Vector3.Dot(Vector3.right, laser.GetLaser()[laser.GetLaser().Count - 1].hitNormal);

        if (vert > 0.0f)
        {
           
            if (hor <= 0.0f)
            {
                return HitSide.TOP;
            }
        }
        if (vert <= 0.0f)
        {
            if (hor > 0.0f)
            {
                return HitSide.RIGHT;
            }
            if (NearlyZero(hor))
            {
                return HitSide.BOTTOM;
            }
            if (hor < 0.0f)
            {
                return HitSide.LEFT;
            }
        }
        return HitSide.TOP;
    }
    Vector3[] FindLaserDirections(Laser laser)
    {
        Vector3[] outDir = new Vector3[2];
        float vert = Vector3.Dot(Vector3.forward, laser.GetLaser()[laser.GetLaser().Count - 1].direction);
        float hor = Vector3.Dot(Vector3.right, laser.GetLaser()[laser.GetLaser().Count - 1].direction);

        
        if (NearlyZero(vert))
        {
            vert = 0.0f;
        }
        if (NearlyZero(hor))
        {
            hor = 0.0f;
        }


        if (vert > 0.0f)
        {
            if (hor > 0.0f)
            {
                //TOP RIGHT 
                outDir[0] = Vector3.forward;
                outDir[1] = Vector3.right;
                return outDir;
            }
            if (hor == 0.0f)
            {
                //TOP
                outDir[0] = (Vector3.forward + Vector3.right).normalized;
                outDir[1] = (Vector3.forward + Vector3.left).normalized;
                return outDir;
            }
            if (hor < 0.0f)
            {
                //TOP LEFT
                outDir[0] = Vector3.forward;
                outDir[1] = Vector3.left;
                return outDir;
            }
        }
        if (vert < 0.0f)
        {
            if (hor > 0.0f)
            {
                //BOTTOM RIGHT
                outDir[0] = Vector3.back;
                outDir[1] = Vector3.right;
                return outDir;
            }
            if (hor == 0.0f)
            {
                //BOTTOM
                outDir[0] = (Vector3.back + Vector3.right).normalized;
                outDir[1] = (Vector3.back + Vector3.left).normalized;
                return outDir;
            }
            if (hor < 0.0f)
            {
                //BOTTOM LEFT
                outDir[0] = Vector3.back;
                outDir[1] = Vector3.left;
                return outDir;
            }
        }
        if (vert == 0.0f)
        {
            if (hor > 0.0f)
            {
                //RIGHT
                outDir[0] = (Vector3.forward + Vector3.right).normalized;
                outDir[1] = (Vector3.back + Vector3.right).normalized;
                return outDir;
            }
            if (Mathf.Approximately(hor, 0.0f))
            {
                outDir[0] = Vector3.zero;
                outDir[1] = Vector3.zero;
                return outDir;
            }
            if (hor < 0.0f)
            {
                //LEFT
                outDir[0] = (Vector3.forward + Vector3.left).normalized;
                outDir[1] = (Vector3.back + Vector3.left).normalized;
                return outDir;
            }
        }
        outDir[0] = Vector3.zero;
        outDir[1] = Vector3.zero;
        return outDir;
    }

    Vector3[] FindStartPositions(Laser hitLaser)
    {
        Vector3[] outPositions = new Vector3[2];
        float vert = Vector3.Dot(Vector3.forward, hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].direction);
        float hor = Vector3.Dot(Vector3.right, hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].direction);


        if (NearlyZero(vert))
        {
            vert = 0.0f;
        }
        if (NearlyZero(hor))
        {
            hor = 0.0f;
        }


        if (vert > 0.0f)
        {
            if (hor > 0.0f)
            {
                //TOP RIGHT 
                outPositions[0] = Vector3.forward;
                outPositions[1] = Vector3.right;
                return outPositions;
            }
            if (hor == 0.0f)
            {
                //TOP
                outPositions[0] = Vector3.forward;
                outPositions[1] = Vector3.forward ;
                return outPositions;
            }
            if (hor < 0.0f)
            {
                //TOP LEFT
                outPositions[0] = Vector3.forward;
                outPositions[1] = Vector3.left;
                return outPositions;
            }
        }
        if (vert < 0.0f)
        {
            if (hor > 0.0f)
            {
                //BOTTOM RIGHT
                outPositions[0] = Vector3.back;
                outPositions[1] = Vector3.right;
                return outPositions;
            }
            if (hor == 0.0f)
            {
                //BOTTOM
                outPositions[0] = Vector3.back;
                outPositions[1] = Vector3.back;
                return outPositions;
            }
            if (hor < 0.0f)
            {
                //BOTTOM LEFT
                outPositions[0] = Vector3.back;
                outPositions[1] = Vector3.left;
                return outPositions;
            }
        }
        if (vert == 0.0f)
        {
            if (hor > 0.0f)
            {
                //RIGHT
                outPositions[0] = Vector3.right;
                outPositions[1] = Vector3.right;
                return outPositions;
            }
            if (Mathf.Approximately(hor, 0.0f))
            {
                outPositions[0] = Vector3.zero;
                outPositions[1] = Vector3.zero;
                return outPositions;
            }
            if (hor < 0.0f)
            {
                //LEFT
                outPositions[0] =  Vector3.left;
                outPositions[1] =  Vector3.left;
                return outPositions;
            }
        }

        outPositions[0] = Vector3.zero;
        outPositions[1] = Vector3.zero;
        return outPositions;
    }

    bool NearlyZero(float a)
    {
        Vector2 _a = a * Vector2.up;
        if (Vector2.Distance(_a, Vector2.zero) < 0.0001)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
