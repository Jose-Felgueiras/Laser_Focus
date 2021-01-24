using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Tower", menuName = "Towers/Tower")]
public class Tower : ScriptableObject
{
    [SerializeField]
    private string description;
    [SerializeField]
    private GameObject towerMesh;
    [SerializeField]
    private int towerCost;
    [SerializeField]
    private Sprite towerImage;
    [SerializeField]
    private Color colorTint;

    [SerializeField]
    private TowerBehaviour behaviour;

    
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
    public TowerBehaviour GetBehaviour()
    {
        return behaviour;
    }
    public string GetDescription()
    {
        return description;
    }
}
