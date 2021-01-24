using UnityEngine;
using System.Data;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using SimpleJSON;

public class Register : MonoBehaviour
{
    public TMP_Text errorText;
    public TMP_InputField inputField;
    private int minUsernameSize = 4;

    public void OnClickButton()
    {
        if (inputField.text.Length >= minUsernameSize)
        {
            StartCoroutine(RegisterUser(inputField.text));
        }
        else
        {
            StopAllCoroutines();
            errorText.text = "Username must have at least 4 letters";
            errorText.color = Color.red;
            StartCoroutine(FadeText(1.0f, 2.0f));
        }
    }


    IEnumerator RegisterUser(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/laserFocus/phpsqliteconnect/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                SceneLoader.instance.CONNECTIONERROR("Connection Error: Unknown Response");

            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                int code;
                if (int.TryParse(jsonArray, out code))
                {
                    switch (code)
                    {
                        case 0:
                            //NETWORK ERROR
                            SceneLoader.instance.CONNECTIONERROR("Connection Error: Unknown Response");
                            break;
                        case 1:
                            //USERNAME ALREADY EXISTS
                            StopAllCoroutines();
                            errorText.text = "Username already exists";
                            errorText.color = Color.red;
                            StartCoroutine(FadeText(1.0f, 2.0f));
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    StartCoroutine(GetUserData(username));
                    //GET PLAYER INFO

                    //SET CONFIG

                    //SEND PLAYER INTO GAME

                }

                //PlayerConfig.SetUsername("AAAA");
                //PlayerConfig.Load();
                //ADD CALLBACK FUNCTION TO PASS RESULT
            }


        }
    }

    IEnumerator GetUserData(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/laserFocus/phpsqliteconnect/GetUserDaTA.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                int code;
                if (int.TryParse(jsonArray, out code))
                {
                    if (code == 0)
                    {
                        SceneLoader.instance.CONNECTIONERROR("Connection Error: Unknown Response");
                    }
                }
                else
                {
                    JSONObject playerJSON = (JSONObject)JSON.Parse(jsonArray);

                    PlayerConfig.SetID(playerJSON["id"]);
                    PlayerConfig.SetUsername(playerJSON["username"]);

                    PlayerConfig.Save();

                    SceneLoader.instance.LoadLevel(2);
                    //GET PLAYER INFO

                    //SET CONFIG

                    //SEND PLAYER INTO GAME

                    //StopAllCoroutines();
                    //errorText.text = "ERROR: " + jsonArray;
                    //errorText.color = Color.red;
                    //StartCoroutine(FadeText(1.0f, 2.0f));
                }
                //PlayerConfig.SetUsername("AAAA");
                //PlayerConfig.Load();
                //ADD CALLBACK FUNCTION TO PASS RESULT
            }
        }
    }

    IEnumerator FadeText(float fadeDuration, float fadeStartDelay)
    {
        float t = 0.0f;
        t -= fadeStartDelay;
        while (t <= 0.0f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        while (t <= 1.0f)
        {
            t += Time.deltaTime / fadeDuration;
            errorText.color = new Color(errorText.color.r, errorText.color.g, errorText.color.b, Mathf.SmoothStep(1.0f, 0.0f, t));

            yield return null;
        }

    }
}
