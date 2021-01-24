using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHUD : MonoBehaviour
{
    [SerializeField]
    private Transform deckInterfaceParent;

    void Start()
    {
        foreach (Transform child in deckInterfaceParent)
        {
            if (child.GetChild(0).GetComponentInChildren<Image>())
            {
                child.GetChild(0).GetComponentInChildren<Image>().sprite = GameManager.instance.GetPlayerDeck().GetTower(child.GetSiblingIndex()).GetSprite();
                child.GetChild(0).GetComponentInChildren<Image>().color = GameManager.instance.GetPlayerDeck().GetTower(child.GetSiblingIndex()).GetColor();
            }
        }
    }
}
