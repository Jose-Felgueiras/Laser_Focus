using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Reflect Behaviour", menuName = "Towers/Tower Behavior/Reflect/Bottom_Right")]
public class ReflectBehaviour : TowerBehaviour
{



    public override void OnLaserHit(Vector2 inDirection, Vector2 position)
    {
        base.OnLaserHit(inDirection, position);

        //Debug.DrawRay(position, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward, Color.red, 5.0f);
        //Debug.DrawRay(position, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right, Color.red, 5.0f);


        if (hitDirections.DOWN)
        {
            GameObject laserFrag = new GameObject("Laser");
            laserFrag.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
            laserFrag.transform.localPosition = Vector3.zero;
            laserFrag.AddComponent<LineRenderer>();
            laserFrag.GetComponent<LineRenderer>().positionCount = 2;
            laserFrag.GetComponent<LineRenderer>().startWidth = 0.2f;
            laserFrag.GetComponent<LineRenderer>().endWidth = 0.2f;
            switch (hitSide)
            {
                case HitSide.TOP:
                    if (!outLasers.LEFT_LEFT)
                    {
                        laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.up * 0.5f);
                        outLasers.LEFT_LEFT = true;

                        laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);

                        GameManager.instance.AddLaser(laserFrag);


                        GameObject laserObj = new GameObject("Laser");
                        laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
                        laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position;
                        laserObj.AddComponent<LineRenderer>();
                        laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
                        laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
                        Laser laser = laserObj.AddComponent<Laser>();
                        Vector2 outDir = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right;
                        laser.SetStartDirection(outDir);
                        laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);
                        laser.SetStartDDAPoint(position + outDir);
                        GameManager.instance.AddLaser(laser.gameObject);
                    }
                    else
                    {
                        Destroy(laserFrag);
                    }
                    break;
                case HitSide.RIGHT:
                    if (!outLasers.UP_UP)
                    {
                        laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.right * 0.5f);
                        outLasers.UP_UP = true;

                        laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);

                        GameManager.instance.AddLaser(laserFrag);

                        GameObject laserObj = new GameObject("Laser");
                        laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
                        laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position;
                        laserObj.AddComponent<LineRenderer>();
                        laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
                        laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
                        Laser laser = laserObj.AddComponent<Laser>();
                        Vector2 outDir = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right;
                        laser.SetStartDirection(outDir);
                        laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);
                        laser.SetStartDDAPoint(position + outDir);
                        GameManager.instance.AddLaser(laser.gameObject);
                    }
                    else
                    {
                        Destroy(laserFrag);
                    }
                    break;
                case HitSide.BOTTOM:
                    if (!outLasers.RIGHT_RIGHT)
                    {
                        laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.down * 0.5f);
                        outLasers.RIGHT_RIGHT = true;

                        laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);

                        GameManager.instance.AddLaser(laserFrag);

                        GameObject laserObj = new GameObject("Laser");
                        laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
                        laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position;
                        laserObj.AddComponent<LineRenderer>();
                        laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
                        laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
                        Laser laser = laserObj.AddComponent<Laser>();
                        Vector2 outDir = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right;
                        laser.SetStartDirection(outDir);
                        laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);
                        laser.SetStartDDAPoint(position + outDir);
                        GameManager.instance.AddLaser(laser.gameObject);
                    }
                    else
                    {
                        Destroy(laserFrag);
                    }
                    break;
                case HitSide.LEFT:
                    if (!outLasers.DOWN_DOWN)
                    {
                        laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.left * 0.5f);
                        outLasers.DOWN_DOWN = true;

                        laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);

                        GameManager.instance.AddLaser(laserFrag);

                        GameObject laserObj = new GameObject("Laser");
                        laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
                        laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position;
                        laserObj.AddComponent<LineRenderer>();
                        laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
                        laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
                        Laser laser = laserObj.AddComponent<Laser>();
                        Vector2 outDir = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right;
                        laser.SetStartDirection(outDir);
                        laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);
                        laser.SetStartDDAPoint(position + outDir);
                        GameManager.instance.AddLaser(laser.gameObject);
                    }
                    else
                    {
                        Destroy(laserFrag);
                    }
                    break;
                default:
                    break;
            }
        }

        if (hitDirections.RIGHT)
        {
            GameObject laserFrag = new GameObject("Laser");
            laserFrag.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
            laserFrag.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position;
            laserFrag.AddComponent<LineRenderer>();
            laserFrag.GetComponent<LineRenderer>().positionCount = 2;
            laserFrag.GetComponent<LineRenderer>().startWidth = 0.2f;
            laserFrag.GetComponent<LineRenderer>().endWidth = 0.2f;
            switch (hitSide)
            {
                case HitSide.TOP:
                    if (!outLasers.RIGHT_RIGHT)
                    {
                        laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.up * 0.5f);
                        outLasers.RIGHT_RIGHT = true;

                        laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);

                        GameManager.instance.AddLaser(laserFrag);

                        GameObject laserObj = new GameObject("Laser");
                        laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
                        laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position - GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward;
                        laserObj.AddComponent<LineRenderer>();
                        laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
                        laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
                        Laser laser = laserObj.AddComponent<Laser>();
                        Vector2 outDir = -GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward;
                        laser.SetStartDirection(outDir);
                        laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);
                        laser.SetStartDDAPoint(position + outDir);
                        GameManager.instance.AddLaser(laser.gameObject);
                    }
                    else
                    {
                        Destroy(laserFrag);
                    }
                    break;
                case HitSide.RIGHT:
                    if (!outLasers.DOWN_DOWN)
                    {
                        laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.right * 0.5f);
                        outLasers.DOWN_DOWN = true;

                        laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);

                        GameManager.instance.AddLaser(laserFrag);

                        GameObject laserObj = new GameObject("Laser");
                        laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
                        laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position - GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward;
                        laserObj.AddComponent<LineRenderer>();
                        laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
                        laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
                        Laser laser = laserObj.AddComponent<Laser>();
                        Vector2 outDir = -GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward;
                        laser.SetStartDirection(outDir);
                        laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);
                        laser.SetStartDDAPoint(position + outDir);
                        GameManager.instance.AddLaser(laser.gameObject);
                    }
                    else
                    {
                        Destroy(laserFrag);
                    }
                    break;
                case HitSide.BOTTOM:
                    if (!outLasers.LEFT_LEFT)
                    {
                        laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.down * 0.5f);
                        outLasers.LEFT_LEFT = true;

                        laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);

                        GameManager.instance.AddLaser(laserFrag);

                        GameObject laserObj = new GameObject("Laser");
                        laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
                        laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position - GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward;
                        laserObj.AddComponent<LineRenderer>();
                        laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
                        laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
                        Laser laser = laserObj.AddComponent<Laser>();
                        Vector2 outDir = -GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward;
                        laser.SetStartDirection(outDir);
                        laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);
                        laser.SetStartDDAPoint(position + outDir);
                        GameManager.instance.AddLaser(laser.gameObject);
                    }
                    else
                    {
                        Destroy(laserFrag);
                    }
                    break;
                case HitSide.LEFT:
                    if (!outLasers.UP_UP)
                    {
                        laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.left * 0.5f);
                        outLasers.UP_UP = true;

                        laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);

                        GameManager.instance.AddLaser(laserFrag);

                        GameObject laserObj = new GameObject("Laser");
                        laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
                        laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position - GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward;
                        laserObj.AddComponent<LineRenderer>();
                        laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
                        laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
                        Laser laser = laserObj.AddComponent<Laser>();
                        Vector2 outDir = -GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward;
                        laser.SetStartDirection(outDir);
                        laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position);
                        laser.SetStartDDAPoint(position + outDir);
                        GameManager.instance.AddLaser(laser.gameObject);
                    }
                    else
                    {
                        Destroy(laserFrag);
                    }
                    break;
                default:
                    break;
            }


        }

        if (hitDirections.DOWN_LEFT)
        {
            
            GameObject laserFrag = new GameObject("Laser Fragment");
            laserFrag.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
            laserFrag.transform.localPosition = Vector3.zero;
            laserFrag.AddComponent<LineRenderer>();
            laserFrag.GetComponent<LineRenderer>().positionCount = 2;
            laserFrag.GetComponent<LineRenderer>().startWidth = 0.2f;
            laserFrag.GetComponent<LineRenderer>().endWidth = 0.2f;

            GameManager.instance.AddLaser(laserFrag);

            GameObject laserObj = new GameObject("Laser");
            laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
            laserObj.AddComponent<LineRenderer>();
            laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
            laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
            Laser laser = laserObj.AddComponent<Laser>();
            if (localHitSide == ELocalHitSide.BOTTOM)
            {
                switch (hitSide)
                {
                    case HitSide.TOP:
                        if (!outLasers.LEFT_DOWN_LEFT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);

                            laserObj.transform.name = "LEFT_DOWN_LEFT";

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f;
                            laser.SetStartDirection(inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);
                            laser.SetStartDDAPoint(position + (Vector2.right * (-0.5f)));
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.LEFT_DOWN_LEFT = true;
                        }
                        else
                        {
                            Destroy(laserFrag);
                            Destroy(laserObj);
                        }

                        break;
                    case HitSide.RIGHT:
                        if (!outLasers.UP_UP_LEFT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);

                            laserObj.transform.name = "UP_UP_LEFT";

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * 0.5f;
                            laser.SetStartDirection(inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);
                            laser.SetStartDDAPoint(position + (Vector2.up));

                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.UP_UP_LEFT = true;
                        }
                        else
                        {
                            Destroy(laserFrag);
                            Destroy(laserObj);
                        }

                        break;
                    case HitSide.BOTTOM:
                        if (!outLasers.RIGHT_UP_RIGHT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);

                            laserObj.transform.name = "RIGHT_UP_RIGHT";
                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f;
                            laser.SetStartDirection(inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);
                            laser.SetStartDDAPoint(position + (Vector2.right * (0.5f)));

                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.RIGHT_UP_RIGHT = true;
                        }
                        else
                        {
                            Destroy(laserFrag);
                            Destroy(laserObj);
                        }

                        break;
                    case HitSide.LEFT:
                        if (!outLasers.DOWN_DOWN_RIGHT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);

                            laserObj.transform.name = "DOWN_DOWN_RIGHT";

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * 0.5f;
                            laser.SetStartDirection(inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);
                            laser.SetStartDDAPoint(position + (Vector2.right * (0.25f)));

                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.DOWN_DOWN_RIGHT = true;
                        }
                        else
                        {
                            Destroy(laserFrag);
                            Destroy(laserObj);
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Destroy(laserFrag);
                Destroy(laserObj);
            }
        }

        if (hitDirections.UP_RIGHT)
        {
            GameObject laserFrag = new GameObject("Laser Fragment");
            laserFrag.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
            laserFrag.transform.localPosition = Vector3.zero;
            laserFrag.AddComponent<LineRenderer>();
            laserFrag.GetComponent<LineRenderer>().positionCount = 2;
            laserFrag.GetComponent<LineRenderer>().startWidth = 0.2f;
            laserFrag.GetComponent<LineRenderer>().endWidth = 0.2f;

            GameManager.instance.AddLaser(laserFrag);

            GameObject laserObj = new GameObject("Laser");
            laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
            laserObj.AddComponent<LineRenderer>();
            laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
            laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
            Laser laser = laserObj.AddComponent<Laser>();

            if (localHitSide == ELocalHitSide.RIGHT)
            {
                switch (hitSide)
                {
                    case HitSide.TOP:
                        if (!outLasers.RIGHT_DOWN_RIGHT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);

                            laserObj.transform.name = "RIGHT_DOWN_RIGHT";

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * -0.5f;
                            laser.SetStartDirection(inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);
                            laser.SetStartDDAPoint(position + Vector2.up + Vector2.right);
                            laser.SetCheckFirstCell(false);
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.RIGHT_DOWN_RIGHT = true;
                        }
                        else
                        {
                            Destroy(laserFrag);
                            Destroy(laserObj);
                        }
                        break;
                    case HitSide.RIGHT:
                        if (!outLasers.DOWN_DOWN_LEFT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);

                            laserObj.transform.name = "DOWN_DOWN_LEFT";

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.debugColor = Color.white;
                            laser.SetStartDirection(inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);
                            laser.SetStartDDAPoint(position);

                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.DOWN_DOWN_LEFT = true;
                        }
                        else
                        {
                            Destroy(laserFrag);
                            Destroy(laserObj);
                        }
                        break;
                    case HitSide.BOTTOM:
                        if (!outLasers.LEFT_UP_LEFT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);

                            laserObj.transform.name = "LEFT_UP_LEFT";

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.SetStartDirection(inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);
                            laser.SetStartDDAPoint(position);
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.LEFT_UP_LEFT = true;

                        }
                        else
                        {
                            Destroy(laserFrag);
                            Destroy(laserObj);
                        }

                        break;
                    case HitSide.LEFT:
                        if (!outLasers.UP_UP_RIGHT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f);
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);

                            laserObj.transform.name = "UP_UP_RIGHT";

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.SetStartDirection(inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward * -0.5f);
                            laser.SetStartDDAPoint(position + (Vector2.left * 0.5f));
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.UP_UP_RIGHT = true;
                        }
                        else
                        {
                            Destroy(laserFrag);
                            Destroy(laserObj);
                        }

                        break;
                    default:
                        Destroy(laserFrag);
                        Destroy(laserObj);
                        break;
                }
            }
            else
            {
                Destroy(laserFrag);
                Destroy(laserObj);
            }
            
        }

        if (hitDirections.DOWN_RIGHT)
        {
            GameObject laserFrag = new GameObject("Lase Fragment");
            laserFrag.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
            laserFrag.transform.localPosition = Vector3.zero;
            laserFrag.AddComponent<LineRenderer>();
            laserFrag.GetComponent<LineRenderer>().positionCount = 2;
            laserFrag.GetComponent<LineRenderer>().startWidth = 0.2f;
            laserFrag.GetComponent<LineRenderer>().endWidth = 0.2f;

            GameObject laserObj = new GameObject("Laser");
            laserObj.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);
            laserObj.AddComponent<LineRenderer>();
            laserObj.GetComponent<LineRenderer>().startWidth = 0.2f;
            laserObj.GetComponent<LineRenderer>().endWidth = 0.2f;
            Laser laser = laserObj.AddComponent<Laser>();
            Vector3 pos2;
            float dot;
            switch (hitSide)
            {
                case HitSide.TOP:
                    dot = Vector2.Dot(Vector2.right, inDirection);

                    if (dot > 0)
                    {

                        if (!outLasers.UP_UP_LEFT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.up * 0.5f);
                            pos2 = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.up * 0.5f + new Vector3(inDirection.x, inDirection.y, 0) / 2;
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, pos2);

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.SetStartDirection(-inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.up * 0.5f);
                            laser.SetStartDDAPoint(position + (Vector2.right * 1.25f) + (Vector2.up * 0.75f));
                            laser.SetCheckFirstCell(false);
                            laser.debugColor = Color.white;

                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.UP_UP_LEFT = true;
                        }
                    }
                    else
                    {
                        if (!outLasers.UP_UP_RIGHT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.up * 0.5f);
                            pos2 = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.up * 0.5f + new Vector3(inDirection.x, inDirection.y, 0) / 2;
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, pos2);

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.SetStartDirection(-inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.up * 0.5f);
                            laser.SetStartDDAPoint(position);
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.UP_UP_RIGHT = true;
                        }
                    }
                    break;
                case HitSide.RIGHT:
                    dot = Vector2.Dot(Vector2.up, inDirection);
                    if (dot > 0)
                    {
                        if (!outLasers.RIGHT_DOWN_RIGHT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.right * 0.5f);
                            pos2 = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.right * 0.5f + new Vector3(inDirection.x, inDirection.y, 0) / 2;
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, pos2);

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.SetStartDirection(-inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.right * 0.5f);
                            laser.SetStartDDAPoint(position + Vector2.up + Vector2.right);
                            laser.SetCheckFirstCell(false);
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.RIGHT_DOWN_RIGHT = true;
                        }
                    }
                    else
                    {
                        if (!outLasers.RIGHT_UP_RIGHT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.right * 0.5f);
                            pos2 = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.right * 0.5f + new Vector3(inDirection.x, inDirection.y, 0) / 2;
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, pos2);

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.SetStartDirection(-inDirection);
                            laser.debugColor = Color.white;
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.right * 0.5f);
                            laser.SetStartDDAPoint(position + (Vector2.down * 0.5f));
                            laser.SetCheckFirstCell(false);
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.RIGHT_UP_RIGHT = true;
                        }
                    }
                    break;
                case HitSide.BOTTOM:
                    dot = Vector2.Dot(Vector2.right, inDirection);
                    Debug.Log(dot);
                    if (dot > 0)
                    {
                        if (!outLasers.DOWN_DOWN_LEFT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.down * 0.5f);
                            pos2 = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.down * 0.5f + new Vector3(inDirection.x, inDirection.y, 0) / 2;
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, pos2);

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.SetStartDirection(-inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.down * 0.5f);
                            laser.SetStartDDAPoint(position + (Vector2.right * 0.5f));
                            laser.SetCheckFirstCell(false);
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.DOWN_DOWN_LEFT = true;
                        }
                        
                    }
                    else
                    {
                        if (!outLasers.DOWN_DOWN_RIGHT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.down * 0.5f);
                            pos2 = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.down * 0.5f + new Vector3(inDirection.x, inDirection.y, 0) / 2;
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, pos2);

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.SetStartDirection(-inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.down * 0.5f);
                            laser.SetStartDDAPoint(position + Vector2.up);
                            laser.SetCheckFirstCell(false);
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.DOWN_DOWN_RIGHT = true;
                        }
                    }


                    break;
                case HitSide.LEFT:
                    dot = Vector2.Dot(Vector2.up, inDirection);
                    Debug.Log(dot);
                    if (dot > 0)
                    {
                        if (!outLasers.LEFT_DOWN_LEFT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.left * 0.5f);
                            pos2 = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.left * 0.5f + new Vector3(inDirection.x, inDirection.y, 0) / 2;
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, pos2);

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.SetStartDirection(-inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.left * 0.5f);
                            laser.SetStartDDAPoint(position + (Vector2.up * 0.5f));
                            laser.SetCheckFirstCell(false);
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.LEFT_DOWN_LEFT = true;
                        }
                        
                    }
                    else
                    {
                        if (!outLasers.LEFT_UP_LEFT)
                        {
                            laserFrag.GetComponent<LineRenderer>().SetPosition(0, GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.left * 0.5f);
                            pos2 = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.left * 0.5f + new Vector3(inDirection.x, inDirection.y, 0) / 2;
                            laserFrag.GetComponent<LineRenderer>().SetPosition(1, pos2);

                            laserObj.transform.position = GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right * 0.5f;
                            laser.SetStartDirection(-inDirection);
                            laser.SetStartPosition(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.position + Vector3.left * 0.5f);
                            laser.SetStartDDAPoint(position + (Vector2.right * 0.5f));
                            laser.SetCheckFirstCell(false);
                            GameManager.instance.AddLaser(laser.gameObject);
                            outLasers.LEFT_UP_LEFT = true;
                        }
                    }

                    break;
                default:
                    break;
            }

            GameManager.instance.AddLaser(laserFrag);

        }
    }
}
