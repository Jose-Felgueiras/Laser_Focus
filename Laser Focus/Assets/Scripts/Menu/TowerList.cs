using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerList : MonoBehaviour
{
    public static TowerList towerList;

    [SerializeField]
    private Sprite[] commonRarityButton = new Sprite[2];
    [SerializeField]
    private Sprite[] uncommonRarityButton = new Sprite[2];
    [SerializeField]
    private Sprite[] rareRarityButton = new Sprite[2];
    [SerializeField]
    private Sprite[] epicRarityButton = new Sprite[2];
    [SerializeField]
    private Sprite[] legendaryRarityButton = new Sprite[2];

    public GameObject towerSelectionPrefab;
    public GameObject towersBlocker;


    private int selectedChild = -1;

    private bool canEquip;
    private Tower selectedTower;

    void Awake()
    {
        if (!towerList)
        {
            towerList = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        GameObject row = new GameObject("Row 0");
        row.transform.parent = transform;
        row.AddComponent<HorizontalLayoutGroup>();
        row.GetComponent<RectTransform>().sizeDelta = new Vector2(944, 230);
        row.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
        row.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperCenter;

        row.GetComponent<HorizontalLayoutGroup>().padding.top = 8;
        row.GetComponent<HorizontalLayoutGroup>().padding.left = 8;
        row.GetComponent<HorizontalLayoutGroup>().spacing = 8;
        row.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
        row.GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = false;
        //row.GetComponent<RectTransform>().a

        for (int i = 0; i < AllTowers.instance.GetTowerCount(); i++)
        {
            if (i > 0 && i % 4 == 0)
            {
                GameObject newRow = new GameObject("Row " + Mathf.FloorToInt(i / 4).ToString());
                newRow.transform.parent = transform;
                newRow.AddComponent<HorizontalLayoutGroup>();
                newRow.GetComponent<RectTransform>().sizeDelta = new Vector2(944, 230);
                newRow.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                newRow.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperCenter;

                newRow.GetComponent<HorizontalLayoutGroup>().padding.top = 8;
                newRow.GetComponent<HorizontalLayoutGroup>().padding.left = 8;
                newRow.GetComponent<HorizontalLayoutGroup>().spacing = 8;
                newRow.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
                newRow.GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = false;
            }
            GameObject towerSelection = Instantiate(towerSelectionPrefab,transform.GetChild(Mathf.FloorToInt(i / 4)));
            towerSelection.AddComponent<DragDrop>();
            SpriteState tempState = new SpriteState();
            towerSelection.transform.GetChild(0).GetComponent<Button>().transition = Selectable.Transition.SpriteSwap;
            switch (AllTowers.instance.GetTowerFromIndex(i).GetRarity())
            {
                case ETowerRarity.COMMON:
                    tempState.highlightedSprite = commonRarityButton[0];
                    tempState.selectedSprite = commonRarityButton[0];
                    tempState.pressedSprite = commonRarityButton[1];
                    towerSelection.transform.GetChild(0).GetComponent<Button>().image.sprite = commonRarityButton[0];
                    towerSelection.transform.GetChild(0).GetComponent<Button>().spriteState = tempState;
                    break;
                case ETowerRarity.UNCOMMON:
                    tempState.highlightedSprite = uncommonRarityButton[0];
                    tempState.selectedSprite = uncommonRarityButton[0];
                    tempState.pressedSprite = uncommonRarityButton[1];
                    towerSelection.transform.GetChild(0).GetComponent<Button>().image.sprite = uncommonRarityButton[0];
                    towerSelection.transform.GetChild(0).GetComponent<Button>().spriteState = tempState;
                    break;
                case ETowerRarity.RARE:
                    tempState.highlightedSprite = rareRarityButton[0];
                    tempState.selectedSprite = rareRarityButton[0];
                    tempState.pressedSprite = rareRarityButton[1];
                    towerSelection.transform.GetChild(0).GetComponent<Button>().image.sprite = rareRarityButton[0];
                    towerSelection.transform.GetChild(0).GetComponent<Button>().spriteState = tempState;
                    break;
                case ETowerRarity.EPIC:
                    tempState.highlightedSprite = epicRarityButton[0];
                    tempState.selectedSprite = epicRarityButton[0];
                    tempState.pressedSprite = epicRarityButton[1];
                    towerSelection.transform.GetChild(0).GetComponent<Button>().image.sprite = epicRarityButton[0];
                    towerSelection.transform.GetChild(0).GetComponent<Button>().spriteState = tempState;
                    break;
                case ETowerRarity.LEGENDARY:
                    tempState.highlightedSprite = legendaryRarityButton[0];
                    tempState.selectedSprite = legendaryRarityButton[0];
                    tempState.pressedSprite = legendaryRarityButton[1];
                    towerSelection.transform.GetChild(0).GetComponent<Button>().image.sprite = legendaryRarityButton[0];
                    towerSelection.transform.GetChild(0).GetComponent<Button>().spriteState = tempState;
                    break;
                default:
                    break;
            }
            towerSelection.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = AllTowers.instance.GetTowerFromIndex(i).GetSprite();
            towerSelection.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = AllTowers.instance.GetTowerFromIndex(i).GetColor();
            int index = towerSelection.transform.GetSiblingIndex() + towerSelection.transform.parent.GetSiblingIndex() * 4;
            towerSelection.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { OnSelect(index); });
            towerSelection.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { OnSelectEquip(index); });
            towerSelection.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { OnSelectInfo(index); });
        }
    }

    private void OnSelect(int index)
    {
        if (index != selectedChild)
        {
            if (selectedChild >= 0)
            {
                transform.GetChild((Mathf.FloorToInt(selectedChild / 4))).GetChild(selectedChild % 4).GetChild(1).gameObject.SetActive(false);
                StartCoroutine(SmoothUpdate(new Vector2(944, 318), new Vector2(944, 230), .3f, selectedChild));
                StartCoroutine(SmoothFloat(.7f, 1f, .3f, transform.GetChild((Mathf.FloorToInt(selectedChild / 4))).GetChild(selectedChild % 4).gameObject));
            }
            selectedChild = index;

            transform.GetChild((Mathf.FloorToInt(index / 4))).GetChild(index % 4).GetChild(1).gameObject.SetActive(true);
            StartCoroutine(SmoothUpdate(transform.GetChild((Mathf.FloorToInt(index / 4))).GetComponent<RectTransform>().sizeDelta, new Vector2(944, 318), .3f, selectedChild));
            StartCoroutine(SmoothFloat(1f, .7f, .3f, transform.GetChild((Mathf.FloorToInt(selectedChild / 4))).GetChild(selectedChild % 4).gameObject));

        }
        else
        {
            transform.GetChild((Mathf.FloorToInt(selectedChild / 4))).GetChild(selectedChild % 4).GetChild(1).gameObject.SetActive(false);
            StartCoroutine(SmoothUpdate(new Vector2(944, 318), new Vector2(944, 230), .3f, selectedChild));
            StartCoroutine(SmoothFloat(.7f, 1f, .3f, transform.GetChild((Mathf.FloorToInt(selectedChild / 4))).GetChild(selectedChild % 4).gameObject));
            selectedChild = -1;
        }
    }
    private void OnSelectEquip(int index)
    {
        transform.GetChild((Mathf.FloorToInt(index / 4))).GetChild(index % 4).GetChild(1).gameObject.SetActive(false);
        StartCoroutine(SmoothUpdate(new Vector2(944, 318), new Vector2(944, 230), .3f, index));
        StartCoroutine(SmoothFloat(.7f, 1f, .3f, transform.GetChild((Mathf.FloorToInt(index / 4))).GetChild(index % 4).gameObject));
        selectedChild = -1;
        towersBlocker.SetActive(true);
        selectedTower = AllTowers.instance.GetTowerFromIndex(index);
        canEquip = true;
    }
    private void OnSelectInfo(int index)
    {
        Debug.Log("Equip " + AllTowers.instance.GetTowerFromIndex(index).GetDescription());

    }
    public void CancelEquip()
    {
        selectedTower = null;
        towersBlocker.SetActive(false);
        canEquip = false;
    }
    public bool CanEquip()
    {
        return canEquip;
    }
    public Tower GetSelectedTower()
    {
        return selectedTower;
    }

    #region Coroutines

    private IEnumerator SmoothFloat(float startPos, float endPos, float time, GameObject obj)
    {
        float t = 0.0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime / time;
            obj.GetComponent<AspectRatioFitter>().aspectRatio = Mathf.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }

    private IEnumerator SmoothUpdate(Vector2 startPos, Vector2 endPos, float time, int index)
    {
        float t = 0.0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime / time;
            transform.GetChild((Mathf.FloorToInt(index / 4))).GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());

            yield return null;
        }
    }

    #endregion

    //transform.GetChild(index).GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    //transform.GetChild(index).GetChild(1).gameObject.SetActive(true);
}


