using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class MenuTouchManager : MonoBehaviour
{

    #region Private Serializable Fields

    [SerializeField]
    private float maxSwipeTime;
    [SerializeField]
    private float minSwipeDistance;

    #endregion

    #region Private Fields

    private float swipeStartTime;
    private float swipeEndTime;
    private float swipeTime;


    private Vector2 startSwipePosition;
    private Vector2 endSwipePosition;
    private float swipeLength;

    private string swipe;
    #endregion
    #region GUI

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);

        GUI.Label(new Rect(20, 20, (float)Screen.width / 2.0f, (float)Screen.height / 2.0f * 0.25f),
           swipe);
        //GUI.Label(new Rect(20, 20, width, height * 0.25f), hitName.ToString());

    }

    #endregion

    #region Public Methods

    #endregion
}
