using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reflect Behaviour", menuName = "Towers/Tower Behavior/Reflect")]
public class ReflectBehaviour : TowerBehaviour
{
    private LaserPoint outLaserPoint;

    public override void OnLaserHit(Laser hitLaser, GridTile hitTower)
    {
        //hitLaser.GetLaser().Count
        Debug.DrawRay(hitTower.currentTowerMesh.transform.position, hitTower.currentTowerMesh.transform.forward, Color.red, 1000.0f);
        Debug.DrawRay(hitTower.currentTowerMesh.transform.position, hitTower.currentTowerMesh.transform.right, Color.red, 1000.0f);


        outLaserPoint.position = hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].hitPoint + hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].hitNormal * 0.000001f;
        outLaserPoint.direction = Vector3.Reflect(hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].direction, hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].hitNormal);
        RaycastHit hit;
        if (Physics.Raycast(outLaserPoint.position, outLaserPoint.direction, out hit, Mathf.Infinity))
        {

            outLaserPoint.hitPoint = hit.point;
            outLaserPoint.hitNormal = hit.normal;
            outLaserPoint.hitTower = hit.transform.gameObject;
        }
        else
        {
            outLaserPoint.position = hitLaser.GetLaser()[hitLaser.GetLaser().Count - 1].position;
        }

        hitLaser.GetLaser().Add(outLaserPoint);

    }
}
