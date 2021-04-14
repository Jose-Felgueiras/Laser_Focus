using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    private Deck[] deckList = new Deck[3];
    private int selectedDeck;

    [SerializeField]
    private Sprite[] commonRarityButton = new Sprite[2];
    [SerializeField]
    private Sprite[] uncommonRarityButton = new Sprite[2];
    [SerializeField]
    private Sprite[] rareRarityButton = new Sprite[2];
    [SerializeField]
    private Sprite[] epicRarityButton = new Sprite[2];
    [SerializeField]
    private Sprite[] legendaryRarityButton= new Sprite[2];

    public void Start()
    {
        deckList[0] = new Deck();
        deckList[0].SetDeck(0);
        deckList[1] = new Deck();
        deckList[1].SetDeck(1);
        deckList[2] = new Deck();
        deckList[2].SetDeck(2);

        SelectDeck(PlayerConfig.GetSelectedDeck());
    }

    private void UpdateDeckImages()
    {
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            if (deckList[selectedDeck].GetDeck()[i])
            {
                transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>().sprite = deckList[selectedDeck].GetDeck()[i].GetSprite();
                transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>().color = deckList[selectedDeck].GetDeck()[i].GetColor();
                transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().transition = Selectable.Transition.SpriteSwap;

                SpriteState tempState = new SpriteState();
                switch (deckList[selectedDeck].GetDeck()[i].GetRarity())
                {
                    case ETowerRarity.COMMON:
                        transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().image.sprite = commonRarityButton[0];
                        tempState.highlightedSprite = commonRarityButton[0];
                        tempState.selectedSprite = commonRarityButton[0];
                        tempState.pressedSprite = commonRarityButton[1];

                        transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().spriteState = tempState;
                        break;
                    case ETowerRarity.UNCOMMON:
                        transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().image.sprite = uncommonRarityButton[0];

                        tempState.highlightedSprite = uncommonRarityButton[0];
                        tempState.selectedSprite = uncommonRarityButton[0];
                        tempState.pressedSprite = uncommonRarityButton[1];

                        transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().spriteState = tempState;
                        break;
                    case ETowerRarity.RARE:
                        transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().image.sprite = rareRarityButton[0];

                        tempState.highlightedSprite = rareRarityButton[0];
                        tempState.selectedSprite = rareRarityButton[0];
                        tempState.pressedSprite = rareRarityButton[1];

                        transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().spriteState = tempState;
                        break;
                    case ETowerRarity.EPIC:
                        transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().image.sprite = epicRarityButton[0];

                        tempState.highlightedSprite = epicRarityButton[0];
                        tempState.selectedSprite = epicRarityButton[0];
                        tempState.pressedSprite = epicRarityButton[1];

                        transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().spriteState = tempState;
                        break;
                    case ETowerRarity.LEGENDARY:
                        transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().image.sprite = legendaryRarityButton[0];

                        tempState.highlightedSprite = legendaryRarityButton[0];
                        tempState.selectedSprite = legendaryRarityButton[0];
                        tempState.pressedSprite = legendaryRarityButton[1];

                        transform.GetChild(1).GetChild(i).gameObject.GetComponent<Button>().spriteState = tempState;
                        break;
                    default:
                        break;
                }
                
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
        transform.GetComponentInParent<DeckManager>().gameObject.transform.parent.GetComponentInChildren<TowerList>().CancelEquip();
        PlayerConfig.SetDeck(selectedDeck, deckList[selectedDeck].GetDecksIndices());
    }

    public void SelectDeck(int index)
    {
        transform.GetChild(0).GetChild(0).GetChild(selectedDeck).GetComponent<Button>().interactable = true;
        selectedDeck = index;
        PlayerConfig.SetSelectedDeck(selectedDeck);
        transform.GetChild(0).GetChild(0).GetChild(selectedDeck).GetComponent<Button>().interactable = false;
        UpdateDeckImages();
    }

    public void ClickedSlot(int index)
    {
        if (transform.GetComponentInParent<DeckManager>().gameObject.transform.parent.GetComponentInChildren<TowerList>().CanEquip())
        {
            EquipTower(index, transform.GetComponentInParent<DeckManager>().gameObject.transform.parent.GetComponentInChildren<TowerList>().GetSelectedTower());
        }
    }
}
