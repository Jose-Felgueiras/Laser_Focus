using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Android;


public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IInitializePotentialDragHandler
{
    #region Private Fields Serializable

    [Header("Swiping Settings")]
    [SerializeField]
    private float percentThreshold = 0.2f;
    [SerializeField]
    private float easeSpeed = 0.5f;

    [Header("Bottom Buttons")]
    [SerializeField]
    private Transform bottomButtonRow;


    [Header("Tower Tab Settings")]
    [SerializeField]
    private float draggThreshold = 0.1f;
    [SerializeField]
    private float towersPannelYOffset = 310f;


    [Header("Friends Tab Settings")]
    [SerializeField]
    private GameObject friendsMovablePanel;

    #endregion

    #region Private Fields
    //Swiping Settings
    private Vector3 startPanelLocation;
    private Vector3 panelLocation;
    private int panels = 5;
    private int currentPanel = 0;

    //Input Settings
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private float startTouchTime;
    private float endTouchTime;

    //Towers Tab Settings
    private Vector3 allTowersPanelStart;
    private Vector3 allTowersPanel;
    private GameObject tempGO;
    private Tower draggedTower;

    //Friends Tab Settings
    private Vector3 friendsPanelStart;
    private Vector3 friendsPanel;


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        startPanelLocation = transform.position;
        panelLocation = transform.position;
        allTowersPanelStart =  new Vector3(transform.GetChild(currentPanel).GetChild(0).transform.localPosition.x, transform.GetChild(currentPanel).GetChild(0).transform.position.y, transform.GetChild(currentPanel).GetChild(0).transform.localPosition.z) - new Vector3(0, towersPannelYOffset, 0);
        allTowersPanel = allTowersPanelStart;

        friendsPanelStart = friendsMovablePanel.transform.position;
        friendsPanel = friendsPanelStart;
        ChangeToPanel(2);
    }


    #region Drag Handlers

    public void OnInitializePotentialDrag(PointerEventData data)
    {
        startTouchTime = Time.time;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        startTouchPos = data.position;
        if (data.pointerCurrentRaycast.gameObject)
        {
            if (data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<AspectRatioFitter>())
            {
                if (!tempGO)
                {
                    tempGO = new GameObject("Temp");
                    tempGO.transform.parent = transform.parent;
                    tempGO.AddComponent<Image>();
                    int index = data.pointerCurrentRaycast.gameObject.transform.parent.GetSiblingIndex() + data.pointerCurrentRaycast.gameObject.transform.parent.parent.GetSiblingIndex() * 4;
                    draggedTower = AllTowers.instance.GetTowerFromIndex(index);
                    tempGO.GetComponent<Image>().sprite = draggedTower.GetSprite();
                    tempGO.GetComponent<Image>().color = draggedTower.GetColor();
                    tempGO.GetComponent<Image>().raycastTarget = false;
                }
            }
        }
    }


    public void OnDrag(PointerEventData data)
    {
        if (tempGO)
        {
            if ((startTouchTime - Time.time) >= draggThreshold)
            {
                tempGO.transform.position = data.position;
                return;
                
            }
            Destroy(tempGO);
            tempGO = null;
        }

        float differenceX = data.pressPosition.x - data.position.x;
        float differenceY = data.pressPosition.y - data.position.y;
        float percent = (data.pressPosition.x - data.position.x) / Screen.width;
        Vector2 lenght = data.pressPosition - data.position;
        float dot = Vector2.Dot(lenght.normalized, Vector2.up);

        if (!(Mathf.Abs(dot) >= 0.7f))
        {
            differenceX /= panels;
            transform.position = panelLocation - new Vector3(differenceX, 0, 0);
        }  
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (tempGO)
        {
            if (data.pointerCurrentRaycast.gameObject)
            {
                if (data.pointerCurrentRaycast.gameObject.GetComponentInParent<DeckManager>())
                {
                    if (data.pointerCurrentRaycast.gameObject.GetComponentInParent<GridLayoutGroup>())
                    {
                        data.pointerCurrentRaycast.gameObject.GetComponentInParent<DeckManager>().EquipTower(data.pointerCurrentRaycast.gameObject.GetComponentInParent<Button>().gameObject.transform.GetSiblingIndex(), draggedTower);
                        Debug.Log(data.pointerCurrentRaycast.gameObject.GetComponentInParent<Button>().gameObject.name);
                    }
                }
            }
            Destroy(tempGO);
            tempGO = null;
            draggedTower = null;
            return;
        }

        endTouchPos = data.position;
        endTouchTime = Time.time;
        Vector2 direction = endTouchPos - startTouchPos;
        float dot = Vector2.Dot(direction.normalized, Vector2.up);
        float speed = Vector2.Distance(startTouchPos, endTouchPos) / (endTouchTime - startTouchTime);
        float percent = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(dot) <= 0.7f)
        {
            if (Mathf.Abs(percent) >= percentThreshold)
            {
                Vector3 newLocation = panelLocation;
                if (percent > 0)
                {
                    if (currentPanel < panels - 1)
                    {
                        ChangeToPanel(currentPanel + 1);
                    }
                }
                else
                {
                    if (percent < 0)
                    {
                        if (currentPanel > 0)
                        {
                            ChangeToPanel(currentPanel - 1);
                        }
                    }
                }
                newLocation = new Vector3(-Screen.width * currentPanel, startPanelLocation.y, startPanelLocation.z) + new Vector3(Screen.width / 2, 0, 0);

                StartCoroutine(SmoothMove(transform.position, newLocation, easeSpeed));
                panelLocation = newLocation;
            }
            else
            {
                StartCoroutine(SmoothMove(transform.position, panelLocation, easeSpeed));
            }
        }
    }

    #endregion
    

    public void ChangeToPanel(int panelIndex)
    {
        if (panelIndex == 3)
        {
            ClientSend.RequestAllFriendRequestsList();
            ClientSend.RequestAllFriendsList();
        }
        if (panelIndex>= 0 && panelIndex < panels)
        {
            bottomButtonRow.GetChild(currentPanel).GetComponent<Button>().interactable = true;
            currentPanel = panelIndex;
            Vector3 newLocation = new Vector3(-Screen.width * currentPanel, startPanelLocation.y, startPanelLocation.z) + new Vector3(Screen.width / 2, 0, 0);
            StartCoroutine(SmoothMove(transform.position, newLocation, easeSpeed));
            panelLocation = newLocation;

            bottomButtonRow.GetChild(panelIndex).GetComponent<Button>().interactable = false;
        }
    }

    #region Private Coroutines

    private IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float time)
    {
        float t = 0.0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime / time;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
    #endregion
}
