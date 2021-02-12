using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FriendRequestEntry : MonoBehaviour
{
    #region Private Serializable Fields



    #endregion

    #region Private Fields

    private int id;
    private string username;

    private TMP_Text usernameText;

    private Button acceptButton;
    private Button rejectButton;

    #endregion

    #region Private Methods

    private void UpdateEntryVisuals()
    {
        usernameText.text = username;
    }

    #endregion

    #region Public Methods

    public void AcceptFriendRequest()
    {
        Debug.Log("Acepting...");
        ClientSend.AcceptFriendRequest(id);
    }

    public void RejectFriendRequest()
    {
        Debug.Log("Rejecting...");
        ClientSend.RejectFriendRequest(id);

    }

    public void SettupEntry(int _id, string _username)
    {
        usernameText = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();

        acceptButton = transform.GetChild(1).GetChild(0).GetComponent<Button>();
        rejectButton = transform.GetChild(1).GetChild(1).GetComponent<Button>();

        acceptButton.onClick.AddListener(AcceptFriendRequest);
        rejectButton.onClick.AddListener(RejectFriendRequest);

        id = _id;
        username = _username;

        UpdateEntryVisuals();

    }

    public int GetEntryID()
    {
        return id;
    }
    public void SetEntryID(int _id)
    {
        id = _id;
    }
    public string GetEntryUsername()
    {
        return username;
    }

    public void SetEntryUsername(string _username)
    {
        username = _username;
    }
    #endregion
}
