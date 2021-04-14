using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map 1", menuName = "Maps/Map 1")]
public class Map_1 : MapBase
{



    // 17x13
    public override GridTile[,] GenerateMap(int sizeX, int sizeY, Transform holder)
    {
        grid = new GridTile[sizeX, sizeY];
        mapLasers = new Laser[1];
        
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                grid[x, y].position = new Vector2(x, y);

                grid[x, y].background = Instantiate(mapGenerationTowers[0].GetGameObject(), holder);
                grid[x, y].background.transform.position = new Vector3(grid[x, y].position.x, grid[x, y].position.y, 0) + offset;
                grid[x, y].background.isStatic = true;

                if ((x+y) % 2 == 0)
                {
                    grid[x, y].background.GetComponent<MeshRenderer>().material.color = Color.black;
                }
                if (x == 0 || y == 0 || x == sizeX - 1 || y == sizeY - 1)
                {
                    grid[x, y].currentTower = mapGenerationTowers[1];
                    grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[1].GetGameObject(), grid[x, y].background.transform);
                    grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, grid[x, y].position.y, -1) + offset;
                    grid[x, y].currentTowerMesh.GetComponent<MeshRenderer>().material.color = Color.red;
                    grid[x, y].owner = TileOwner.GAMEMANAGER;
                }

                if (x == 4 && y >= 4 && y <= sizeY - (4 + 1) || x == sizeX - (4 + 1) && y >= 4 && y <= sizeY - (4 + 1))
                {
                    grid[x, y].currentTower = mapGenerationTowers[1];
                    grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[1].GetGameObject(), grid[x, y].background.transform);
                    grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, grid[x, y].position.y, -1) + offset;
                    grid[x, y].currentTowerMesh.GetComponent<MeshRenderer>().material.color = Color.red;
                    grid[x, y].owner = TileOwner.GAMEMANAGER;
                }

                //Laser
                if (x == sizeX / 2 && y == sizeY / 2)
                {
                    grid[x, y].currentTower = mapGenerationTowers[4];
                    grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[4].GetGameObject(), grid[x, y].background.transform);
                    grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, grid[x, y].position.y, -1) + offset;
                    grid[x, y].owner = TileOwner.GAMEMANAGER;

                    mapLasers[0] = grid[x, y].currentTowerMesh.GetComponent<Laser>();
                    mapLasers[0].SetStartPosition(grid[x, y].currentTowerMesh.transform.position);
                    mapLasers[0].SetStartDDAPoint(grid[x, y].position);
                    mapLasers[0].SetCheckFirstCell(false);
                }

                if (x == playerStartPositions[0].x && y == playerStartPositions[0].y)
                {
                    //Spawn Player 1
                    grid[x, y].currentTower = mapGenerationTowers[1];
                    grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[1].GetGameObject(), grid[x, y].background.transform);
                    grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, grid[x, y].position.y, -1) + offset;

                }
                if (x == playerStartPositions[1].x && y == playerStartPositions[1].y)
                {
                    //Spawn Player 2
                    grid[x, y].currentTower = mapGenerationTowers[1];
                    grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[1].GetGameObject(), grid[x, y].background.transform);
                    grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, grid[x, y].position.y, -1) + offset;
                }
            }
        }
        return grid;
    }
}
