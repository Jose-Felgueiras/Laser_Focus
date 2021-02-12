using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map Testing", menuName = "Maps/Testing")]
public class Map_Testing : MapBase
{

    #region MapBase Callbacks


    //MADE FOR 25x25 WONT WORK FOR OTHER SIZES
    public override GridTile[,] GenerateMap(int sizeX, int sizeY, Transform holder)
    {
        grid = new GridTile[sizeX, sizeY];
        for (int y = 0; y < sizeX; y++)
        {
            for (int x = 0; x < sizeY; x++)
            {
                grid[x, y].position = new Vector2(x, y);


                
                if (x <= 3 && y >= 7 && y <= 17 || x >= 7 && x < 18 && y <= 3 || x >= 7 && x < 18 && y >= 21 || x >=21 && y >= 7 && y <= 17)
                {
                    grid[x, y].background = new GameObject("Empty");
                    grid[x, y].background.transform.parent = holder;
                    //grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[1].GetGameObject(), grid[x, y].background.transform);
                    //grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, 1, grid[x, y].position.y);
                    //grid[x, y].currentTowerMesh.GetComponent<MeshRenderer>().material.color = Color.red;
                    //grid[x, y].currentTower = null;
                }
                else
                {
                    grid[x, y].background = Instantiate(mapGenerationTowers[0].GetGameObject(), holder);
                    grid[x, y].background.transform.position = new Vector3(grid[x, y].position.x, 0, grid[x, y].position.y);
                    grid[x, y].background.isStatic = true;
                }

                //Outside Walls
                if (x == 0 || y == 0 || x == sizeX - 1 || y == sizeY - 1 || x <= 4 && y >= 6 && y <= 18 || x >= 6 && x <= 18 && y <= 4 || x >= 6 && x <= 18 && y >= 20 || x >= 20 && y >= 6 && y <= 18)
                {
                    if (grid[x, y].background.GetComponent<MeshRenderer>())
                    {
                        if (x == 9 || x == 10 || x == 12 || x == 14 || x == 15 || y == 9 || y == 10 || y == 12 || y == 14 || y == 15)
                        {
                            grid[x, y].currentTower = mapGenerationTowers[2];
                            grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[2].GetGameObject(), grid[x, y].background.transform);
                            grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, 1, grid[x, y].position.y);
                            grid[x, y].currentTowerMesh.GetComponent<MeshRenderer>().material.color = Color.blue;
                            grid[x, y].owner = TileOwner.GAMEMANAGER;

                        }
                        else
                        {
                            if (x == 4 && y == 6 || x == 18 && y == 20)
                            {
                                grid[x, y].currentTower = mapGenerationTowers[3];
                                grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[3].GetGameObject(), grid[x, y].background.transform);
                                grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, 1, grid[x, y].position.y);
                                grid[x, y].currentTowerMesh.GetComponent<MeshRenderer>().material.color = Color.blue;
                                grid[x, y].owner = TileOwner.GAMEMANAGER;

                            }
                            else
                            {
                                if (x == 6 && y == 20 || x == 20 && y == 6)
                                {
                                    grid[x, y].currentTower = mapGenerationTowers[3];
                                    grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[3].GetGameObject(), grid[x, y].background.transform);
                                    grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, 1, grid[x, y].position.y);
                                    grid[x, y].currentTowerMesh.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                                    grid[x, y].currentTowerMesh.GetComponent<MeshRenderer>().material.color = Color.blue;
                                    grid[x, y].owner = TileOwner.GAMEMANAGER;

                                }
                                else
                                {
                                    if (x == 6 && y == 4 || x == 20 && y == 18)
                                    {
                                        grid[x, y].currentTower = mapGenerationTowers[3];
                                        grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[3].GetGameObject(), grid[x, y].background.transform);
                                        grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, 1, grid[x, y].position.y);
                                        grid[x, y].currentTowerMesh.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                                        grid[x, y].currentTowerMesh.GetComponent<MeshRenderer>().material.color = Color.blue;
                                        grid[x, y].owner = TileOwner.GAMEMANAGER;

                                    }
                                    else
                                    {
                                        if (x == 4 && y == 18 || x == 18 && y == 4)
                                        {
                                            grid[x, y].currentTower = mapGenerationTowers[3];
                                            grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[3].GetGameObject(), grid[x, y].background.transform);
                                            grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, 1, grid[x, y].position.y);
                                            grid[x, y].currentTowerMesh.transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                                            grid[x, y].currentTowerMesh.GetComponent<MeshRenderer>().material.color = Color.blue;
                                            grid[x, y].owner = TileOwner.GAMEMANAGER;

                                        }
                                        else
                                        {
                                            grid[x, y].currentTower = mapGenerationTowers[1];
                                            grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[0].GetGameObject(), grid[x, y].background.transform);
                                            grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, 1, grid[x, y].position.y);
                                            grid[x, y].currentTowerMesh.GetComponent<MeshRenderer>().material.color = Color.red;
                                            grid[x, y].owner = TileOwner.GAMEMANAGER;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //Laser
                if (x == sizeX / 2 && y == sizeY / 2)
                {
                    grid[x, y].currentTower = mapGenerationTowers[4];
                    grid[x, y].currentTowerMesh = Instantiate(mapGenerationTowers[4].GetGameObject(), grid[x, y].background.transform);
                    grid[x, y].currentTowerMesh.transform.position = new Vector3(grid[x, y].position.x, 1, grid[x, y].position.y);
                    grid[x, y].owner = TileOwner.GAMEMANAGER;
                }
            }
        }
        return grid;
    }

    #endregion

}
