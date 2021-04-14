using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public enum EHitDirections
{
    NULL,
    UP,
    UP_RIGHT,
    RIGHT,
    DOWN_RIGHT,
    DOWN,
    DOWN_LEFT,
    LEFT,
    UP_LEFT
}


public class TowerBehaviour : MonoBehaviour, IBehaviour<Vector2, Vector2>
{
    [Serializable]
    public struct HitDirections
    {
        public bool UP;
        public bool UP_RIGHT;
        public bool RIGHT;
        public bool DOWN_RIGHT;
        public bool DOWN;
        public bool DOWN_LEFT;
        public bool LEFT;
        public bool UP_LEFT;
    }
    [Serializable]
    public struct OutLasers
    {
        public bool UP_UP_LEFT;
        public bool UP_UP;
        public bool UP_UP_RIGHT;
        public bool RIGHT_UP_RIGHT;
        public bool RIGHT_RIGHT;
        public bool RIGHT_DOWN_RIGHT;
        public bool DOWN_DOWN_RIGHT;
        public bool DOWN_DOWN;
        public bool DOWN_DOWN_LEFT;
        public bool LEFT_DOWN_LEFT;
        public bool LEFT_LEFT;
        public bool LEFT_UP_LEFT;
    }

    [Serializable]
    public struct CheckTile
    {
        public bool UP;
        public bool RIGHT;
        public bool DOWN;
        public bool LEFT;
    }
    protected GridTile gridTile;
    protected Laser inLaser;
    protected bool bIsHit = false;
    protected HitDirections hitDirections = new HitDirections { UP = false, UP_RIGHT = false, RIGHT = false, DOWN_RIGHT = false, DOWN = false, DOWN_LEFT = false, LEFT = false, UP_LEFT = false };
    protected CheckTile checkTiles = new  CheckTile{ UP = false, RIGHT = false, DOWN = false, LEFT = false };
    protected Vector2 v2PrevInDirection;
    protected Vector2 v2PrevPosition;
    protected EHitDirections hitDirection;
    protected HitSide hitSide;
    protected ELocalHitSide localHitSide;
    protected OutLasers outLasers;
    public virtual void OnLaserHit(Vector2 inDirection, Vector2 position)
    {
        Debug.DrawLine(position, position + inDirection, Color.red, 109f);
        bIsHit = true;
        v2PrevInDirection = inDirection;
        v2PrevPosition = position;
        hitDirection = GetHitDirection(inDirection, position);
        switch (hitDirection)
        {
            case EHitDirections.NULL:
                break;
            case EHitDirections.UP:
                if (hitDirections.UP)
                    return;
                else
                    hitDirections.UP = true;
                break;
            case EHitDirections.UP_RIGHT:
                if (hitDirections.UP_RIGHT)
                    return;
                else
                    hitDirections.UP_RIGHT = true;
                break;
            case EHitDirections.RIGHT:
                if (hitDirections.RIGHT)
                    return;
                else
                    hitDirections.RIGHT = true;
                break;
            case EHitDirections.DOWN_RIGHT:
                if (hitDirections.DOWN_RIGHT)
                    return;
                else
                    hitDirections.DOWN_RIGHT = true;
                break;
            case EHitDirections.DOWN:
                if (hitDirections.DOWN)
                    return;
                else
                    hitDirections.DOWN = true;
                break;
            case EHitDirections.DOWN_LEFT:
                if (hitDirections.DOWN_LEFT)
                    return;
                else
                    hitDirections.DOWN_LEFT = true;
                break;
            case EHitDirections.LEFT:
                if (hitDirections.LEFT)
                    return;
                else
                    hitDirections.LEFT = true;
                break;
            case EHitDirections.UP_LEFT:
                if (hitDirections.UP_LEFT)
                    return;
                else
                    hitDirections.UP_LEFT = true;
                break;
            default:
                break;
        }
        
    }
    public virtual void OnLaserStopHit(Vector2 inDirection, Vector2 position)
    {

    }
    public virtual void OnStartTurn(Vector2 position)
    {

    }
    public virtual void OnEndTurn(Vector2 position)
    {
        //if (bIsHit && inLaser == null)
        //{
        //    OnLaserStopHit(v2PrevInDirection, v2PrevPosition);
        //}
        //CHECK IF LASER IS STILL HITTING
    }
    public virtual void OnPlace(Vector2 position)
    {
        Debug.Log("Placed " + gridTile.currentTower.name + " on " + gridTile.position.ToString());
    }
    public virtual void OnRemove(Vector2 position)
    {

    }

    public void SetInLaser(Laser laser)
    {
        inLaser = laser;
    }

    public bool IsHit()
    {
        return bIsHit;
    }

    protected EHitDirections GetHitDirection(Vector2 inDirection, Vector2 position)
    {
        float fDotVert = Vector2.Dot(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward, inDirection.normalized);
        float fDotHor = Vector2.Dot(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.right, inDirection.normalized);
        if (fDotVert > 0.95f)
        {
            return EHitDirections.DOWN;
        }
        if (fDotVert < -0.95)
        {
            return EHitDirections.UP;
        }
        if (fDotHor > 0.95f)
        {
            return EHitDirections.LEFT;
        }
        if (fDotHor < -0.95)
        {
            return EHitDirections.RIGHT;
        }
        if (fDotVert > 0.0f && fDotVert <= 0.95f)
        {
            if (fDotHor > 0.0f)
            {
                return EHitDirections.DOWN_LEFT;
            }
            if (fDotHor < 0.0f)
            {
                return EHitDirections.DOWN_RIGHT;
            }
        }
        if (fDotVert < 0.0f && fDotVert >= -0.95f)
        {
            if (fDotHor > 0.0f)
            {
                return EHitDirections.UP_LEFT;
            }
            if (fDotHor < 0.0f)
            {
                return EHitDirections.UP_RIGHT;
            }
        }
        return EHitDirections.NULL;
    }

    public virtual void ClearAllHits()
    {
        hitDirections.UP = false;
        hitDirections.UP_RIGHT = false;
        hitDirections.RIGHT = false;
        hitDirections.DOWN_RIGHT = false;
        hitDirections.DOWN = false;
        hitDirections.DOWN_LEFT = false;
        hitDirections.LEFT = false;
        hitDirections.UP_LEFT = false;

        checkTiles.UP = false;
        checkTiles.RIGHT = false;
        checkTiles.DOWN= false;
        checkTiles.LEFT= false;


        outLasers.UP_UP_LEFT = false;
        outLasers.UP_UP = false;
        outLasers.UP_UP_RIGHT = false;
        outLasers.RIGHT_UP_RIGHT = false;
        outLasers.RIGHT_RIGHT = false;
        outLasers.RIGHT_DOWN_RIGHT = false;
        outLasers.DOWN_DOWN_RIGHT = false;
        outLasers.DOWN_DOWN = false;
        outLasers.DOWN_DOWN_LEFT = false;
        outLasers.LEFT_DOWN_LEFT = false;
        outLasers.LEFT_LEFT = false;
        outLasers.LEFT_UP_LEFT = false;
        bIsHit = false;
    }

    public void SetHitSide(HitSide side)
    {
        hitSide = side;
    }

    public void SetLocalHitSide(ELocalHitSide side)
    {
        localHitSide = side;
    }
    public void SetGridTile(GridTile tile)
    {
        gridTile = tile;
    }
}
