using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollBehaviour : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IInitializePotentialDragHandler
{

    [SerializeField]
    private float easeSpeed = 1.0f;

    [SerializeField]
    private float distanceFromBottom = 0.0f;

    [SerializeField]
    private bool useLocal;

    private Vector3 position;
    private Vector3 startPosition;

    private Vector3 starDragPosition;
    private Vector3 endDragPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (!useLocal)
        {
            startPosition = transform.position;
        }
        else
        {
            startPosition = transform.localPosition;
        }
        position = startPosition;
    }

    public void OnInitializePotentialDrag(PointerEventData data)
    {
        GetComponentInParent<PageSwiper>().OnInitializePotentialDrag(data);
    }

    public void OnBeginDrag(PointerEventData data)
    {
        starDragPosition = data.pressPosition;
    }

    public void OnDrag(PointerEventData data)
    {
        float differenceX = data.pressPosition.x - data.position.x;
        float differenceY = data.pressPosition.y - data.position.y;
        Vector2 lenght = data.pressPosition - data.position;
        float dot = Vector2.Dot(lenght.normalized, Vector2.up);

        if (Mathf.Abs(dot) >= 0.7f)
        {
            if (!useLocal)
            {
                transform.position = new Vector3(transform.position.x, position.y, transform.position.z)   - new Vector3(0, differenceY, 0);
            }
            else
            {
                transform.localPosition= new Vector3(transform.localPosition.x, position.y, transform.localPosition.z) - new Vector3(0, differenceY, 0);
            }
        }
        else
        {
            GetComponentInParent<PageSwiper>().OnDrag(data);
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (!useLocal)
        {
            position = transform.position;
        }
        else
        {
            position = transform.localPosition;
        }
        Vector2 lenght = data.pressPosition - data.position;
        float dot = Vector2.Dot(lenght.normalized, Vector2.up);
        if (Mathf.Abs(dot) >= 0.7f)
        {
            if (position.y < startPosition.y)
            {
                if (!useLocal)
                {
                    StartCoroutine(SmoothMove(transform.position, startPosition, easeSpeed));
                }
                else
                {
                    StartCoroutine(SmoothMove(transform.localPosition, startPosition, easeSpeed));
                }
            }
            if (!useLocal)
            {
                if (transform.position.y > Screen.height + GetComponent<RectTransform>().sizeDelta.y - (Screen.height - distanceFromBottom))
                {
                    Vector3 pos = new Vector3(transform.position.x, (Screen.height + GetComponent<RectTransform>().sizeDelta.y - (Screen.height - distanceFromBottom)), transform.position.z);
                    StartCoroutine(SmoothMove(transform.position, pos, easeSpeed));
                }
            }
            else
            {
                if (transform.localPosition.y > Screen.height + GetComponent<RectTransform>().sizeDelta.y - (Screen.height - distanceFromBottom))
                {
                    Vector3 pos = new Vector3(transform.localPosition.x, (Screen.height + GetComponent<RectTransform>().sizeDelta.y - (Screen.height - distanceFromBottom)), transform.localPosition.z);
                    StartCoroutine(SmoothMove(transform.localPosition, pos, easeSpeed));
                }
            }
        }
        else
        {
            GetComponentInParent<PageSwiper>().OnEndDrag(data);
        }
    }


    private IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float time)
    {
        float t = 0.0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime / time;
            Vector3 pos = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            if (!useLocal)
            {
                transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);
                position = transform.position;

            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, pos.y, transform.localPosition.z);
                position = transform.localPosition;
            }
            yield return null;
        }
    }
}
