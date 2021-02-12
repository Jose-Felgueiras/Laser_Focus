using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchmakingManager : MonoBehaviour
{
    public static MatchmakingManager instance;

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
    [SerializeField]
    private GameObject mathcmakingPanel;

    public int roomID { get; private set; }

    public void JoinOrCreateRandomRoom()
    {
        ClientSend.JoinRandomRoom();
        ShowMatchmakingPanel(true);

    }

    public void CancelMatchmaking()
    {
        ClientSend.CancelMatchmaking();
    }

    public void ShowMatchmakingPanel(bool _value)
    {
        mathcmakingPanel.SetActive(_value);
    }
}
