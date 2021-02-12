using UnityEngine;
using System.Data;
using System;
using System.Text;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using SimpleJSON;

public class Register : MonoBehaviour
{
    public TMP_InputField inputField;
    private int minUsernameSize = 4;

    public void OnClickButton()
    {
        if (inputField.text.Length <= minUsernameSize)
        {
            UIManager.instance.ShowError(1, 3, "Username must have at least 4 letters");
        }
        else
        {
            PlayerConfig.SetUsername(inputField.text);
            ClientSend.RegisterUser(PlayerConfig.GetPlayerUsername());
            UIManager.instance.ShowPanel(false);
            UIManager.instance.ShowLoading(true);

            Debug.Log($"Registering {inputField.text} as a new User");
        }
    }
}
