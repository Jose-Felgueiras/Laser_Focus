using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IInitializePotentialDragHandler
{

    private int selectedTower;
    private GameObject towerHolo;
    private Vector2 gridPos;

    private bool isPlacingTower;

    private GameObject draggTower;

    private bool isPlayerTurn;

    public void OnInitializePotentialDrag(PointerEventData _data)
    {
        if (!isPlacingTower)
        {
            if (_data.pointerPressRaycast.gameObject.GetComponentInParent<Canvas>() && _data.pointerPressRaycast.gameObject.GetComponentInParent<GridLayoutGroup>())
            {
                Debug.Log(_data.pointerPressRaycast.gameObject.transform.name);
                selectedTower = _data.pointerPressRaycast.gameObject.transform.GetSiblingIndex();
                towerHolo = Instantiate(GameManager.instance.GetPlayerDeck().GetTower(selectedTower).GetGameObject());
                towerHolo.transform.position = _data.pointerCurrentRaycast.worldPosition;
                draggTower = towerHolo;
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(_data.position);

            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject == towerHolo)
                {
                    draggTower = towerHolo;
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData _data)
    {

    }

    public void OnDrag(PointerEventData _data)
    {
        if (draggTower)
        {
            Ray ray = Camera.main.ScreenPointToRay(_data.position);

            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                    {
                        gridPos = GameManager.instance.GetGridManager().GetBackgroundCoordsFromIndex(hit.transform.GetSiblingIndex());

                        draggTower.transform.position = new Vector3(gridPos.x, 1, gridPos.y);

                    }
                }
            }
            else
            {
                draggTower.transform.position = new Vector3(_data.pointerCurrentRaycast.worldPosition.x / Screen.width, 1, _data.pointerCurrentRaycast.worldPosition.y / Screen.height);
            }
        }
    }

    public void OnEndDrag(PointerEventData _data)
    {
        if (towerHolo)
        {
            draggTower = null;
            if (GameManager.instance.GetGridManager().IsBackgroundEmpty(gridPos))
            {
                if (isPlayerTurn)
                {
                    StartPlacing();
                }
                else
                {
                    CancelPlacing("Not Your Turn");
                }
            }
            else
            {
                CancelPlacing("Can't Place There");
            }
        }
    }

    public void StartPlacing()
    {
        isPlacingTower = true;
        InGameHUD.instance.StartPlacing();
    }

    public void CancelPlacing()
    {
        isPlacingTower = false;
        Destroy(towerHolo);
        selectedTower = -1;
        InGameHUD.instance.StopPlacing();
    }



    public void CancelPlacing(string _msg)
    {
        isPlacingTower = false;
        Destroy(towerHolo);
        selectedTower = -1;
        InGameHUD.instance.StopPlacing();
        InGameHUD.instance.CallError(_msg, .5f, 1.5f);
    }

    public void RotateTower()
    {
        towerHolo.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
    }

    public void PlaceTowerRequest()
    {
        isPlayerTurn = false;
        ClientSend.PlaceTowerRequest(gridPos, towerHolo.transform.rotation, selectedTower);
        CancelPlacing();
    }

    public void StartTurn()
    {
        InGameHUD.instance.CallError("Your Turn", .5f, 3f);
        isPlayerTurn = true;
    }

    public void Forfeit()
    {
        InGameHUD.instance.HideOptions();
        ClientSend.PlayerForfeited();
        InGameHUD.instance.ShowGameOver("YOU LOSE", "You forfeited");
    }
}
