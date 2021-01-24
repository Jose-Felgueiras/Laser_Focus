using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    private Deck[] deckList = new Deck[3];
    private int selectedDeck;

    public void Start()
    {
        deckList[0] = new Deck();
        deckList[1] = new Deck();
        deckList[2] = new Deck();
        SelectDeck(0);
    }

    private void UpdateDeckImages()
    {
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            if (deckList[selectedDeck].GetDeck()[i])
            {
                transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>().sprite = deckList[selectedDeck].GetDeck()[i].GetSprite();
                transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>().color = deckList[selectedDeck].GetDeck()[i].GetColor();
            }
            else
            {
                transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
                transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
            }
        }
    }
    



    public void EquipTower(int index, Tower tower)
    {
        Debug.LogFormat("Equip Slot {0} with {1} in deck {2}", index, tower.name, selectedDeck);
        deckList[selectedDeck].SetDeckTower(index, tower);
        UpdateDeckImages();
        transform.parent.GetChild(0).GetComponent<TowerList>().CancelEquip();
    }

    public void SelectDeck(int index)
    {
        transform.GetChild(0).GetChild(selectedDeck).GetComponent<Button>().interactable = true;
        selectedDeck = index;
        transform.GetChild(0).GetChild(selectedDeck).GetComponent<Button>().interactable = false;
        UpdateDeckImages();
    }

    public void ClickedSlot(int index)
    {
        if (transform.parent.GetChild(0).GetComponent<TowerList>().CanEquip())
        {
            EquipTower(index, transform.parent.GetChild(0).GetComponent<TowerList>().GetSelectedTower());
        }
    }
}
