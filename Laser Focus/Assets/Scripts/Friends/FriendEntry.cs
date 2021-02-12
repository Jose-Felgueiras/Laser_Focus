using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ConnectionState
{
    CONNECTED,
    INGAME,
    DISCONNECTED
}

public class FriendEntry : MonoBehaviour
{

    #region Private Serizable Fields

    [SerializeField]
    private TMP_Text usernameText;
    [SerializeField]
    private TMP_Text stateText;

    #endregion

    #region Private Fields

    private int id;
    private string username;

    #endregion

    #region Public Fields

    public ConnectionState state;

    #endregion

    #region Private Methods

    private void UpdateEntryVisuals()
    {
        usernameText.text = username;
        switch (state)
        {
            case ConnectionState.CONNECTED:
                stateText.text = "Online";
                stateText.color = Color.green;
                break;
            case ConnectionState.INGAME:
                stateText.text = "In-Game";
                stateText.color = Color.yellow;
                break;
            case ConnectionState.DISCONNECTED:
                stateText.text = "Offline";
                stateText.color = Color.red;
                break;
            default:
                break;
        }
    }

    #endregion

    #region Public Mehtods

    public ConnectionState GetConnectionState()
    {
        return state;
    }

    public void SettupEntry(int _id, string _username, ConnectionState _state)
    {
        id = _id;
        username = _username;
        state = _state;

        UpdateEntryVisuals();
    }

    public int GetEntryID()
    {
        return id;
    }

    public string GetEntryUsername()
    {
        return username;
    }
    #endregion


}
