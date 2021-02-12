using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IInitializePotentialDragHandler
{

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private float startTouchTime;
    private float endTouchTime;

    private GameObject tempGO;
    private Tower draggedTower;

    [SerializeField]
    private float draggThreshold = 0.1f;

    public void OnInitializePotentialDrag(PointerEventData _data)
    {
        startTouchTime = Time.time;
    }

    public void OnBeginDrag(PointerEventData _data)
    {
        if (_data.pointerCurrentRaycast.gameObject)
        {
            if (_data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<AspectRatioFitter>())
            {
                if (!tempGO)
                {
                    tempGO = new GameObject("Temp");
                    tempGO.AddComponent<Image>();
                    tempGO.transform.SetParent(transform.GetComponentInParent<PageSwiper>().transform.parent.transform);
                    int index = _data.pointerCurrentRaycast.gameObject.transform.parent.GetSiblingIndex() + _data.pointerCurrentRaycast.gameObject.transform.parent.parent.GetSiblingIndex() * 4;
                    draggedTower = AllTowers.instance.GetTowerFromIndex(index);
                    tempGO.GetComponent<Image>().sprite = draggedTower.GetSprite();
                    tempGO.GetComponent<Image>().color = draggedTower.GetColor();
                    tempGO.GetComponent<Image>().raycastTarget = false;
                }
            }
        }
        else
        {
            GetComponentInParent<ScrollBehaviour>().OnBeginDrag(_data);
        }
    }

    public void OnDrag(PointerEventData _data)
    {
        if (tempGO)
        {
            if ((Time.time - startTouchTime) >= draggThreshold)
            {
                tempGO.transform.position = _data.position;
                return;

            }
            Destroy(tempGO);
            tempGO = null;
        }
        else
        {
            GetComponentInParent<ScrollBehaviour>().OnDrag(_data);
        }
    }

    public void OnEndDrag(PointerEventData _data)
    {
        if (tempGO)
        {
            if (_data.pointerCurrentRaycast.gameObject)
            {
                if (_data.pointerCurrentRaycast.gameObject.GetComponentInParent<DeckManager>())
                {
                    if (_data.pointerCurrentRaycast.gameObject.GetComponentInParent<GridLayoutGroup>())
                    {
                        _data.pointerCurrentRaycast.gameObject.GetComponentInParent<DeckManager>().EquipTower(_data.pointerCurrentRaycast.gameObject.GetComponentInParent<Button>().gameObject.transform.GetSiblingIndex(), draggedTower);
                        Debug.Log(_data.pointerCurrentRaycast.gameObject.GetComponentInParent<Button>().gameObject.name);
                    }
                }
            }
            Destroy(tempGO.gameObject);
            tempGO = null;
            draggedTower = null;
            return;
        }
        else
        {
            GetComponentInParent<ScrollBehaviour>().OnEndDrag(_data);
        }
    }
}
