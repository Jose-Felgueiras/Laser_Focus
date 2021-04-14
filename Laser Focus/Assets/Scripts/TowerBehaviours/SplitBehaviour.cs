using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SplitBehaviour : TowerBehaviour
{
    #region Private Fields


    #endregion

    #region TowerBehaviour Callbacks

    #endregion
    public override void OnLaserHit(Vector2 inDirection, Vector2 position)
    {
        base.OnLaserHit(inDirection, position);

        if (hitDirections.UP && hitDirection == EHitDirections.UP)
        {
            GenerateSplit(inDirection, position);
        }
        if (hitDirections.UP_RIGHT && hitDirection == EHitDirections.UP_RIGHT)
        {
            GenerateSplit(inDirection, position);
        }
        if (hitDirections.RIGHT && hitDirection == EHitDirections.RIGHT)
        {
            GenerateSplit(inDirection, position);
        }
        if (hitDirections.DOWN_RIGHT && hitDirection == EHitDirections.DOWN_RIGHT)
        {
            GenerateSplit(inDirection, position);
        }
        if (hitDirections.DOWN && hitDirection == EHitDirections.DOWN)
        {
            GenerateSplit(inDirection, position);
        }
        if (hitDirections.DOWN_LEFT && hitDirection == EHitDirections.DOWN_LEFT)
        {
            GenerateSplit(inDirection, position);
        }
        if (hitDirections.LEFT && hitDirection == EHitDirections.LEFT)
        {
            GenerateSplit(inDirection, position);
        }
        if (hitDirections.UP_LEFT && hitDirection == EHitDirections.UP_LEFT)
        {
            GenerateSplit(inDirection, position);
        }
    }

    private void GenerateSplit(Vector2 inDirection, Vector2 position)
    {
        Vector2[] directions = FindLaserDirections(inDirection, position);
        Vector2[] startPositions = FindStartPositions(inDirection);
        Vector2[] startDDAPoints = FindStartDDAPoints(inDirection);

        GameObject split1 = new GameObject("Laser A");
        split1.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);

        GameObject split2 = new GameObject("Laser B");
        split2.transform.SetParent(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform);


        split1.transform.localPosition = new Vector3(startPositions[0].x * .5f, 0, startPositions[0].y * .5f);
        split2.transform.localPosition = new Vector3(startPositions[1].x * .5f, 0 ,startPositions[1].y * .5f);


        Debug.DrawRay(split1.transform.position, directions[0], Color.red, 1000.0f);
        Debug.DrawRay(split2.transform.position, directions[1], Color.red, 1000.0f);

        switch (hitDirection)
        {
            case EHitDirections.NULL:
                break;
            case EHitDirections.UP:
                switch (hitSide)
                {
                    case HitSide.TOP:
                        if (!outLasers.DOWN_DOWN_RIGHT)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.DOWN_DOWN_RIGHT = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.DOWN_DOWN_LEFT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.DOWN_DOWN_LEFT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    case HitSide.RIGHT:
                        break;
                    case HitSide.BOTTOM:
                        
                        break;
                    case HitSide.LEFT:
                        break;
                    default:
                        break;
                }
                break;
            case EHitDirections.UP_RIGHT:
                switch (hitSide)
                {
                    case HitSide.TOP:
                        if (!outLasers.DOWN_DOWN)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.DOWN_DOWN = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.LEFT_LEFT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.LEFT_LEFT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    case HitSide.RIGHT:
                        if (!outLasers.DOWN_DOWN)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.DOWN_DOWN = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.LEFT_LEFT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.LEFT_LEFT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    case HitSide.BOTTOM:
                        
                        break;
                    case HitSide.LEFT:
                        
                        break;
                    default:
                        break;
                }
                break;
            case EHitDirections.RIGHT:
                switch (hitSide)
                {
                    case HitSide.TOP:
                        break;
                    case HitSide.RIGHT:
                        if (!outLasers.LEFT_UP_LEFT)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.LEFT_UP_LEFT = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.LEFT_DOWN_LEFT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.LEFT_DOWN_LEFT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    case HitSide.BOTTOM:
                        break;
                    case HitSide.LEFT:
                        break;
                    default:
                        break;
                }
                break;
            case EHitDirections.DOWN_RIGHT:
                switch (hitSide)
                {
                    case HitSide.TOP:
                        break;
                    case HitSide.RIGHT:
                        if (!outLasers.UP_UP)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.UP_UP = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.LEFT_LEFT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.LEFT_LEFT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    case HitSide.BOTTOM:
                        if (!outLasers.UP_UP)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.UP_UP = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.LEFT_LEFT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.LEFT_LEFT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    case HitSide.LEFT:
                        break;
                    default:
                        break;
                }
                break;
            case EHitDirections.DOWN:
                switch (hitSide)
                {
                    case HitSide.TOP:
                        
                        break;
                    case HitSide.RIGHT:
                        break;
                    case HitSide.BOTTOM:
                        if (!outLasers.UP_UP_LEFT)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.UP_UP_LEFT = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.UP_UP_RIGHT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.UP_UP_RIGHT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    case HitSide.LEFT:
                        break;
                    default:
                        break;
                }
                break;
            case EHitDirections.DOWN_LEFT:
                switch (hitSide)
                {
                    case HitSide.TOP:
                        break;
                    case HitSide.RIGHT:
                        
                        break;
                    case HitSide.BOTTOM:
                        if (!outLasers.UP_UP)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.UP_UP = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.RIGHT_RIGHT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.RIGHT_RIGHT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    case HitSide.LEFT:
                        if (!outLasers.UP_UP)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.UP_UP = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.RIGHT_RIGHT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.RIGHT_RIGHT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    default:
                        break;
                }
                break;
            case EHitDirections.LEFT:
                switch (hitSide)
                {
                    case HitSide.TOP:
                        break;
                    case HitSide.RIGHT:
                        break;
                    case HitSide.BOTTOM:
                        break;
                    case HitSide.LEFT:
                        if (!outLasers.RIGHT_UP_RIGHT)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            //if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            //{
                            //    laser1.SetCheckFirstCell(false);
                            //}

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.RIGHT_UP_RIGHT = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.RIGHT_DOWN_RIGHT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + Vector2.up + Vector2.right);
                            laser2.SetCheckFirstCell(false);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.RIGHT_DOWN_RIGHT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    default:
                        break;
                }
                break;
            case EHitDirections.UP_LEFT:
                switch (hitSide)
                {
                    case HitSide.TOP:
                        if (!outLasers.DOWN_DOWN)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.DOWN_DOWN = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.RIGHT_RIGHT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.RIGHT_RIGHT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    case HitSide.RIGHT:
                        break;
                    case HitSide.BOTTOM:
                        break;
                    case HitSide.LEFT:
                        if (!outLasers.DOWN_DOWN)
                        {
                            split1.AddComponent<LineRenderer>();
                            split1.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split1.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser1 = split1.AddComponent<Laser>();
                            laser1.debugColor = Color.white;
                            laser1.SetStartDirection(directions[0]);
                            laser1.SetStartDDAPoint(position + startDDAPoints[0]);
                            laser1.SetStartPosition(split1.transform.position);

                            //Correct case when laser comes from top
                            if (startDDAPoints[0] == Vector2.up && startDDAPoints[1] == Vector2.zero)
                            {
                                laser1.SetCheckFirstCell(false);
                            }

                            GameManager.instance.AddLaser(laser1.gameObject);
                            outLasers.DOWN_DOWN = true;
                        }
                        else
                        {
                            Destroy(split1);
                        }
                        if (!outLasers.RIGHT_RIGHT)
                        {
                            split2.AddComponent<LineRenderer>();
                            split2.GetComponent<LineRenderer>().startWidth = 0.2f;
                            split2.GetComponent<LineRenderer>().endWidth = 0.2f;
                            Laser laser2 = split2.AddComponent<Laser>();
                            laser2.debugColor = Color.white;
                            laser2.SetStartDirection(directions[1]);
                            laser2.SetStartDDAPoint(position + startDDAPoints[1]);
                            laser2.SetStartPosition(split2.transform.position);


                            GameManager.instance.AddLaser(laser2.gameObject);
                            outLasers.RIGHT_RIGHT = true;
                        }
                        else
                        {
                            Destroy(split2);
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
  
    Vector2[] FindLaserDirections(Vector2 inDirection, Vector2 position)
    {
        Vector2[] outDir = new Vector2[2];
        float vert = Vector2.Dot(Vector2.up, inDirection);
        float hor = Vector2.Dot(Vector2.right, inDirection);


        if (NearlyZero(vert))
        {
            vert = 0.0f;
        }
        if (NearlyZero(hor))
        {
            hor = 0.0f;
        }
        //Debug.Log("Vert: " + vert);
        //Debug.Log("Hor: " + hor);

        if (vert > 0.0f)
        {
            if (hor > 0.0f)
            {
                //TOP RIGHT 
                outDir[0] = Vector2.up;
                outDir[1] = Vector2.right;
                return outDir;
            }
            if (hor == 0.0f)
            {
                //TOP
                outDir[0] = (Vector2.up + (Vector2.right)).normalized;
                outDir[1] = (Vector2.up + Vector2.left).normalized;
                return outDir;
            }
            if (hor < 0.0f)
            {
                //TOP LEFT
                outDir[0] = Vector2.up;
                outDir[1] = Vector2.left;
                return outDir;
            }
        }
        if (vert < 0.0f)
        {
            if (hor > 0.0f)
            {
                //BOTTOM RIGHT
                outDir[0] = Vector2.down;
                outDir[1] = Vector2.right;
                return outDir;
            }
            if (hor == 0.0f)
            {
                //BOTTOM
                outDir[0] = (Vector2.down + Vector2.right).normalized;
                outDir[1] = (Vector2.down + Vector2.left).normalized;
                return outDir;
            }
            if (hor < 0.0f)
            {
                //BOTTOM LEFT
                outDir[0] = Vector2.down;
                outDir[1] = Vector2.left;
                return outDir;
            }
        }
        if (vert == 0.0f)
        {
            if (hor > 0.0f)
            {
                //RIGHT
                outDir[0] = (Vector2.up + Vector2.right).normalized;
                outDir[1] = (Vector2.down + (Vector2.right)).normalized;
                return outDir;
            }
            if (Mathf.Approximately(hor, 0.0f))
            {
                outDir[0] = Vector2.zero;
                outDir[1] = Vector2.zero;
                return outDir;
            }
            if (hor < 0.0f)
            {
                //LEFT
                outDir[0] = ((Vector2.up) + Vector2.left).normalized;
                outDir[1] = (Vector2.down + Vector2.left).normalized;
                return outDir;
            }
        }
        outDir[0] = Vector2.zero;
        outDir[1] = Vector2.zero;
        return outDir;
    }

    Vector2[] FindStartPositions(Vector2 inDirection)

    {
        Vector2[] outPos = new Vector2[2];
        Vector2 direction = inDirection.normalized;
        float vert = Vector2.Dot(direction, Vector2.up);
        float horz = Vector2.Dot(direction, Vector2.right);
        if (vert == 1)
        {
            outPos[0] = Vector2.up;
            outPos[1] = Vector2.up;
            return outPos;
        }
        if (vert == -1)
        {
            outPos[0] = Vector2.down;
            outPos[1] = Vector2.down;

            return outPos;

        }
        if (horz == 1)
        {
            outPos[0] = Vector2.right;
            outPos[1] = Vector2.right;
            return outPos;

        }
        if (horz == -1)
        {
            outPos[0] = Vector2.left;
            outPos[1] = Vector2.left;
            return outPos;

        }

        if (vert > 0)
        {
            if (horz > 0)
            {
                outPos[0] = Vector2.up;
                outPos[1] = Vector2.right;
                return outPos;

            }
            if (horz < 0)
            {
                outPos[0] = Vector2.up;
                outPos[1] = Vector2.left;
                return outPos;
            }
        }

        if (vert < 0)
        {
            if (horz > 0)
            {
                outPos[0] = Vector2.down;
                outPos[1] = Vector2.right;
                return outPos;

            }
            if (horz < 0)
            {
                outPos[0] = Vector2.down;
                outPos[1] = Vector2.left;
                return outPos;
            }
        }
        outPos[0] = Vector2.zero;
        outPos[1] = Vector2.zero;
        return outPos;
    }

    Vector2[] FindStartDDAPoints(Vector2 inDirection)
    {
        Vector2[] outPos = new Vector2[2];
        Vector2 direction = inDirection.normalized;
        float vert = Vector2.Dot(direction, Vector2.up);
        float horz = Vector2.Dot(direction, Vector2.right);
        if (vert == 1)
        {
            outPos[0] = Vector2.up;
            outPos[1] = Vector2.up;
            return outPos;
        }
        if (vert == -1)
        {
            //Case Correction
            outPos[0] = Vector2.up;
            outPos[1] = Vector2.zero;
            return outPos;

        }
        if (horz == 1)
        {
            outPos[0] = Vector2.right;
            outPos[1] = Vector2.right;
            return outPos;

        }
        if (horz == -1)
        {               
            //Case Correction
            outPos[0] = Vector2.zero;
            outPos[1] = Vector2.left;
            return outPos;

        }

        if (vert > 0)
        {
            if (horz > 0)
            {
                outPos[0] = Vector2.up;
                outPos[1] = Vector2.right;
                return outPos;

            }
            if (horz < 0)
            {
                outPos[0] = Vector2.up;
                outPos[1] = Vector2.left;
                return outPos;
            }
        }

        if (vert < 0)
        {
            if (horz > 0)
            {
                outPos[0] = Vector2.down;
                outPos[1] = Vector2.right;
                return outPos;

            }
            if (horz < 0)
            {
                outPos[0] = Vector2.down;
                outPos[1] = Vector2.left;
                return outPos;
            }
        }
        outPos[0] = Vector2.zero;
        outPos[1] = Vector2.zero;
        return outPos;
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
