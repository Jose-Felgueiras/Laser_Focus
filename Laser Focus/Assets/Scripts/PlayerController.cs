using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IInitializePotentialDragHandler
{

    private int selectedTower = -1;
    private GameObject towerHolo;
    private Vector2 gridPos;

    private Vector2 prevPos;

    private bool isPlacingTower;

    private bool isPlayerTurn;

    public void OnInitializePotentialDrag(PointerEventData _data)
    {
        if (!isPlacingTower)
        {
            if (_data.pointerPressRaycast.gameObject.GetComponentInParent<Canvas>() && _data.pointerPressRaycast.gameObject.GetComponentInParent<GridLayoutGroup>())
            {
                selectedTower = _data.pointerPressRaycast.gameObject.transform.GetSiblingIndex();
            }
        }
        else
        {
            if (!_data.pointerPressRaycast.gameObject.GetComponentInParent<HorizontalLayoutGroup>())
            {
                if (towerHolo)
                {
                    towerHolo.SetActive(false);
                }
            }
            if (selectedTower >= 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(_data.position);

                RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

                if (hits.Length > 0)
                {
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                        {
                            if ((prevPos.x + prevPos.y) % 2 == 0)
                            {
                                GameManager.instance.GetGridManager().GetGridTile(prevPos).background.GetComponent<MeshRenderer>().material.color = Color.black;

                            }
                            else
                            {
                                GameManager.instance.GetGridManager().GetGridTile(prevPos).background.GetComponent<MeshRenderer>().material.color = Color.white;
                            }


                            gridPos = GameManager.instance.GetGridManager().GetBackgroundCoordsFromIndex(hit.transform.GetSiblingIndex());

                            GameManager.instance.GetGridManager().GetGridTile(gridPos).background.GetComponent<MeshRenderer>().material.color = Color.cyan;

                            prevPos = gridPos;
                        }
                    }
                }
            }
            //Ray ray = Camera.main.ScreenPointToRay(_data.position);

            //RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

            //foreach (RaycastHit hit in hits)
            //{
            //    Debug.Log(hit.transform.gameObject);
            //    if (hit.transform.gameObject == towerHolo)
            //    {
            //        draggTower = towerHolo;
            //    }
            //}
        }
    }

    public void OnBeginDrag(PointerEventData _data)
    {

    }

    public void OnDrag(PointerEventData _data)
    {
        if (selectedTower >= 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(_data.position);

            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                    {
                        if ((prevPos.x + prevPos.y) % 2 == 0)
                        {
                            GameManager.instance.GetGridManager().GetGridTile(prevPos).background.GetComponent<MeshRenderer>().material.color = Color.black;

                        }
                        else
                        {
                            GameManager.instance.GetGridManager().GetGridTile(prevPos).background.GetComponent<MeshRenderer>().material.color = Color.white;
                        }

                        gridPos = GameManager.instance.GetGridManager().GetBackgroundCoordsFromIndex(hit.transform.GetSiblingIndex());

                        GameManager.instance.GetGridManager().GetGridTile(gridPos).background.GetComponent<MeshRenderer>().material.color = Color.cyan;

                        prevPos = gridPos;
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData _data)
    {
        if (selectedTower >= 0)
        {
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
        InGameHUD.instance.ShowRotateOption(GameManager.instance.GetPlayerDeck().GetTower(selectedTower).CanRotate());
        if (towerHolo)
        {
            towerHolo.SetActive(true);
            towerHolo.transform.position = GameManager.instance.GetGridManager().GetGridTile(gridPos).background.transform.position + Vector3.back;
        }
        else
        {
            towerHolo = Instantiate(GameManager.instance.GetPlayerDeck().GetTower(selectedTower).GetGameObject());
            towerHolo.transform.position = GameManager.instance.GetGridManager().GetGridTile(gridPos).background.transform.position + Vector3.back;
        }
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
        //GameManager.instance.PlaceTower(PlayerConfig.GetPlayerID(), gridPos, towerHolo.transform.rotation, selectedTower);
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
