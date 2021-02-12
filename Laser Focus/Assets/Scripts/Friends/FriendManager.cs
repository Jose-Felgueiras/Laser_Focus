using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FriendManager : MonoBehaviour
{
    #region Singleton

    public static FriendManager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region Private Serializable Fields

    [SerializeField]
    private GameObject addFriendsPanel;

    [SerializeField]
    private TMP_Text idText;

    [SerializeField]
    private TMP_InputField friendRequestID;

    [SerializeField]
    private GameObject popUpPanel;
    [SerializeField]
    private TMP_Text popUpMessage;

    [Header("Friends List")]
    [SerializeField]
    private Transform friendEntryHolder;
    [SerializeField]
    private GameObject friendEntryPrefab;

    [Header("Friend Requests List")]
    [SerializeField]
    private Transform friendRequestEntryHolder;
    [SerializeField]
    private GameObject friendRequestEntryPrefab;
    #endregion

    #region Private Fields

    private List<FriendEntry> friendsList = new List<FriendEntry>();
    private List<FriendRequestEntry> friendRequestsList = new List<FriendRequestEntry>();

    #endregion

    #region Public Methods

    public void ShowFriendsPanel(bool _value)
    {
        addFriendsPanel.SetActive(_value);
        idText.text = PlayerConfig.GetPlayerID().ToString();
        foreach (FriendRequestEntry friend in friendRequestsList)
        {
        }
    }

    public void SendFriendRequest()
    {
        if (friendRequestID.text.Length > 0)
        {
            int requestID = int.Parse(friendRequestID.text);
            if (requestID > 0)
            {
                ClientSend.SendFriendRequest(requestID);
                Debug.Log("Friend Request Sent");
            }
        }
    }

    public void PopUpMessage(string _msg)
    {
        popUpPanel.SetActive(true);
        popUpMessage.text = _msg;
    }

    public void SetFriendsList(int _length)
    {

        for (int i = 0; i < friendEntryHolder.childCount; i++)
        {
            Destroy(friendEntryHolder.GetChild(i).gameObject);
        }
        for (int i = 0; i < _length; i++)
        {
            GameObject _entry = Instantiate(friendEntryPrefab, friendEntryHolder);
            friendsList.Add(_entry.GetComponent<FriendEntry>());
        }
        
    }
    public void PopulateFriendsList(int _id, string _username, int _index)
    {
        friendsList[_index].SettupEntry(_id, _username, ConnectionState.CONNECTED);
    }

    public void SetFriendRequestsList(int _length)
    {
        for (int i = 0; i < friendRequestEntryHolder.childCount; i++)
        {
            Destroy(friendRequestEntryHolder.GetChild(i).gameObject);
        }
        for (int i = 0; i < _length; i++)
        {
            GameObject _entry = Instantiate(friendRequestEntryPrefab, friendRequestEntryHolder);
            friendRequestsList.Add(_entry.GetComponent<FriendRequestEntry>());
        }
    }

    public void PopulateFriendRequestsList(int _id, string _username, int _index)
    {
        friendRequestsList[_index].SettupEntry(_id, _username);
    }

    #endregion
}
