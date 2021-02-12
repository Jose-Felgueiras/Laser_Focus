using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsList : MonoBehaviour
{
    #region Private Fields

    private List<FriendEntry> friendsList = new List<FriendEntry>();
    

    #endregion

    #region Public Methods

    public void SortFriends()
    {
        friendsList.Sort(SortByState);
    }

    #endregion


    static int SortByState(FriendEntry f1, FriendEntry f2)
    {
        return f1.state.CompareTo(f2.state);
    }
}
