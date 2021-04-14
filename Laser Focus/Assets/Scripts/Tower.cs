using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public enum HitSide
{
    TOP, RIGHT, BOTTOM, LEFT
}

public enum ELocalHitSide
{
    TOP,
    RIGHT,
    BOTTOM,
    LEFT
}
[Serializable]
public enum ETowerRarity
{
   COMMON, UNCOMMON, RARE, EPIC, LEGENDARY
}
[CreateAssetMenu(fileName = "New Tower", menuName = "Towers/Tower")]
public class Tower : ScriptableObject
{
    [SerializeField]
    private string description;
    [SerializeField]
    private GameObject towerMesh;
    [SerializeField]
    private ETowerRarity towerRarity;
    [SerializeField]
    private int towerCost;
    [SerializeField]
    private Sprite towerImage;
    [SerializeField]
    private Color colorTint;
    [SerializeField]
    private bool canRotate;
    [SerializeField]
    private TowerBehaviour[] behaviours = new TowerBehaviour[0];

    
    public Sprite GetSprite()
    {
        return towerImage;
    }
    public GameObject GetGameObject()
    {
        return towerMesh;
    }
    public Color GetColor()
    {
        return colorTint;
    }
    public TowerBehaviour[] GetBehaviours()
    {
        return behaviours;
    }
    public void SetBehaviours(TowerBehaviour[] newBehaviours)
    {
        behaviours = newBehaviours;
    }
    public void SetBehaviours(int index, TowerBehaviour newBehaviour)
    {
        behaviours[index] = newBehaviour;
    }
    public string GetDescription()
    {
        return description;
    }
    public bool CanRotate()
    {
        return canRotate;
    }

    public ETowerRarity GetRarity()
    {
        return towerRarity;
    }

    public HitSide FindHitSide(Vector2 mapCheck, Vector2 prevMapCheck)
    {

        if (mapCheck.y > prevMapCheck.y)
        {
            return HitSide.BOTTOM;
        }
        if (mapCheck.y < prevMapCheck.y)
        {
            return HitSide.TOP;
        }
        if (mapCheck.y == prevMapCheck.y)
        {
            if (mapCheck.x >= prevMapCheck.x)
            {
                return HitSide.LEFT;
            }
            if (mapCheck.x < prevMapCheck.x)
            {
                return HitSide.RIGHT;
            }
        }
        return HitSide.BOTTOM;
        //if (mapCheck.x >= prevMapCheck.x)
        //{
        //    if (mapCheck.y >= prevMapCheck.y)
        //    {
        //        return HitSide.BOTTOM;
        //    }
        //    else
        //    {
        //        if (mapCheck.y < prevMapCheck.y)
        //        {
        //            return HitSide.TOP;
        //        }
        //        else
        //        {
        //            if (mapCheck.y == prevMapCheck.y)
        //            {
        //                return HitSide.LEFT;
        //            }
        //        }
        //    }
        //}
        //return HitSide.RIGHT;
    }

    public ELocalHitSide FindLocalHitSide(HitSide hitSide, Vector2 position)
    {
        float dot_vert = Vector3.Dot(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward, Vector3.up);
        float dot_horz = Vector3.Dot(GameManager.instance.GetGridManager().GetGridTile(position).currentTowerMesh.transform.forward, Vector3.right);
        //Debug.Log("dot_vert : " + dot_vert);
        //Debug.Log("dot_horz : " + dot_horz);
        //Debug.Log(hitSide);
        if (dot_vert > 0)
        {
            switch (hitSide)
            {
                case HitSide.TOP:
                    return ELocalHitSide.TOP;
                case HitSide.RIGHT:
                    return ELocalHitSide.RIGHT;
                case HitSide.BOTTOM:
                    return ELocalHitSide.BOTTOM;
                case HitSide.LEFT:
                    return ELocalHitSide.LEFT;
                default:
                    break;
            }
        }
        else
        {
            if (dot_vert < 0)
            {
                switch (hitSide)
                {
                    case HitSide.TOP:
                        return ELocalHitSide.BOTTOM;
                    case HitSide.RIGHT:
                        return ELocalHitSide.LEFT;
                    case HitSide.BOTTOM:
                        return ELocalHitSide.TOP;
                    case HitSide.LEFT:
                        return ELocalHitSide.RIGHT;
                    default:
                        break;
                }
            }
            else
            {
                if (dot_horz > 0)
                {
                    switch (hitSide)
                    {
                        case HitSide.TOP:
                            return ELocalHitSide.LEFT;
                        case HitSide.RIGHT:
                            return ELocalHitSide.TOP;
                        case HitSide.BOTTOM:
                            return ELocalHitSide.RIGHT;
                        case HitSide.LEFT:
                            return ELocalHitSide.BOTTOM;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (hitSide)
                    {
                        case HitSide.TOP:
                            return ELocalHitSide.RIGHT;
                        case HitSide.RIGHT:
                            return ELocalHitSide.BOTTOM;
                        case HitSide.BOTTOM:
                            return ELocalHitSide.LEFT;
                        case HitSide.LEFT:
                            return ELocalHitSide.TOP;
                        default:
                            break;
                    }
                }
            }
        }
        return ELocalHitSide.BOTTOM;

    }
}
