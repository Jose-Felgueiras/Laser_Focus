using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    private Vector3 position;
    private float width;
    private float height;
    private string hitName = "AAA";
    private Vector3 touchPos;
    private Vector2 gridPos;
    private Tower selectedTower;
    private GameObject towerHolo;

    PointerEventData pointerEventData;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        // Position used for the cube.
        position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);

        GUI.Label(new Rect(20, 20, width, height * 0.25f),
            "x = " + gridPos.x.ToString("f2") +
            ", y = " + gridPos.y.ToString("f2"));
        //GUI.Label(new Rect(20, 20, width, height * 0.25f), hitName.ToString());

    }

    void Update()
    {
        // Handle screen touches.

        //MOUSE DEBUG
        #region MOUSE DEBUG
        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Input.mousePosition;
            pointerEventData = new PointerEventData(EventSystem.current) { position = touchPos };
            List<RaycastResult> raycastResults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (RaycastResult result in raycastResults)
                {
                    //hitName = result.gameObject.transform.GetSiblingIndex().ToString();
                    selectedTower = GameManager.instance.GetPlayerDeck().GetTower(result.gameObject.transform.GetSiblingIndex());
                    towerHolo = Instantiate(selectedTower.GetGameObject());
                    towerHolo.transform.position = new Vector3(Input.mousePosition.x, 1, Input.mousePosition.y);
                }
            }
            else
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

                //if (hits.Length > 0)
                //{
                //    foreach (RaycastHit hit in hits)
                //    {
                //        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                //        {
                //            hitName = hit.transform.name;
                //            gridPos = GameManager.instance.GetGridManager().GetBackgroundCoordsFromIndex(hit.transform.GetSiblingIndex());
                //        }
                //    }
                //}
                //else
                //{
                //    hitName = "AAA";
                //}
            }
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                    {
                        hitName = hit.transform.name;
                        gridPos = GameManager.instance.GetGridManager().GetBackgroundCoordsFromIndex(hit.transform.GetSiblingIndex());

                        if (selectedTower)
                        {
                            towerHolo.transform.position = new Vector3(gridPos.x, 1, gridPos.y);
                        }
                    }
                }
            }
            else
            {
                if (selectedTower)
                {
                    towerHolo.transform.position = new Vector3(Input.mousePosition.x, 1, Input.mousePosition.y);
                }
                hitName = "AAA";
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                    {
                        hitName = hit.transform.name;
                        gridPos = GameManager.instance.GetGridManager().GetBackgroundCoordsFromIndex(hit.transform.GetSiblingIndex());
                    }
                }
            }
            else
            {
                Destroy(towerHolo);
                selectedTower = null;
            }
            if (selectedTower && towerHolo)
            {
                if (GameManager.instance.GetGridManager().IsBackgroundEmpty(gridPos))
                {
                    GameManager.instance.GetGridManager().SetGridTileTower(gridPos, towerHolo, selectedTower);
                    //towerHolo.AddComponent(GameManager.instance.GetGridManager().GetTowerFromCoords(gridPos).GetBehaviour().GetType());
                    selectedTower = null;
                }
                else
                {
                    Destroy(towerHolo);
                    selectedTower = null;
                }
                GameManager.instance.UpdateLasers();
            }
        }

        #endregion

        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //Handle Tower Selection from Deck
                //Touch Input from Canvas only
                pointerEventData = new PointerEventData(EventSystem.current){ position = touchPos };
                List<RaycastResult> raycastResults = new List<RaycastResult>();

                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (RaycastResult result in raycastResults)
                    {
                        hitName = result.gameObject.transform.GetSiblingIndex().ToString();
                        selectedTower = GameManager.instance.GetPlayerDeck().GetTower(result.gameObject.transform.GetSiblingIndex());
                        towerHolo = Instantiate(selectedTower.GetGameObject());
                        towerHolo.transform.position = new Vector3(Input.GetTouch(0).position.x, 1, Input.GetTouch(0).position.y);
                    }
                }
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

                    if (hits.Length > 0)
                    {
                        foreach (RaycastHit hit in hits)
                        {
                            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                            {
                                hitName = hit.transform.name;
                                gridPos = GameManager.instance.GetGridManager().GetBackgroundCoordsFromIndex(hit.transform.GetSiblingIndex());
                            }
                        }
                    }
                    else
                    {
                        hitName = "AAA";
                    }
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

                if (hits.Length > 0)
                {
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Background"))
                        {
                            hitName = hit.transform.name;
                            gridPos = GameManager.instance.GetGridManager().GetBackgroundCoordsFromIndex(hit.transform.GetSiblingIndex());

                            if (selectedTower)
                            {
                                towerHolo.transform.position = new Vector3(gridPos.x, 1, gridPos.y);
                            }
                        }
                    }
                }
                else
                {
                    if (selectedTower)
                    {
                        towerHolo.transform.position = new Vector3(Input.GetTouch(0).position.x, 1, Input.GetTouch(0).position.y);
                    }
                    hitName = "AAA";
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (selectedTower && towerHolo)
                {
                    if (GameManager.instance.GetGridManager().IsBackgroundEmpty(gridPos))
                    {
                        GameManager.instance.GetGridManager().SetGridTileTower(gridPos, towerHolo, selectedTower);
                        towerHolo.AddComponent(GameManager.instance.GetGridManager().GetTowerFromCoords(gridPos).GetBehaviour().GetType());
                    }
                    else
                    {
                        Destroy(towerHolo);
                        selectedTower = null;
                    }
                    GameManager.instance.UpdateLasers();
                }
            }
        }
    }
}
