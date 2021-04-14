using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBehaviour : TowerBehaviour
{
    [SerializeField]
    private static float radius = 3;

    public override void OnLaserHit(Vector2 inDirection, Vector2 position)
    {
        base.OnLaserHit(inDirection, position);
        CheckArea(radius, position);


    }


    private void Update()
    {

    }
    void CheckArea(float radius, Vector2 point)
    {
        int r = (int)((radius * 2) - 1);
        for (int x = 0; x < r; x++)
        {
            for (int y = 0; y < r; y++)
            {
                if (x == 0 && y == 0 || x == 0 && y == r - 1 || x == r - 1 && y == 0 || x == r - 1 && y == r - 1)
                {
                    continue;
                }

                Vector2 checkPoint = new Vector2(point.x + x - radius + 1, point.y + y - radius + 1);

                if (checkPoint.x < GameManager.instance.GetGridManager().GetGridDimensions().x && checkPoint.y < GameManager.instance.GetGridManager().GetGridDimensions().y && checkPoint.x >= 0 && checkPoint.y >= 0)
                {
                    if (Vector2.Distance(point + new Vector2(.5f,.5f), checkPoint + new Vector2(.5f, .5f)) <= radius)
                    {
                        GameManager.instance.GetGridManager().GetGridTile(checkPoint).background.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                        if (GameManager.instance.GetGridManager().GetGridTile(checkPoint).owner != TileOwner.GAMEMANAGER)
                        {
                            if (!GameManager.instance.GetGridManager().IsTileAvailable(checkPoint))
                            {
                                GameManager.instance.GetGridManager().ClearGridTileTower(checkPoint);
                            }
                        }
                    }
                }

                
            }
        }
        GameManager.instance.GetGridManager().ClearLaserHits();
        GameManager.instance.UpdateLasers();
    }
}
