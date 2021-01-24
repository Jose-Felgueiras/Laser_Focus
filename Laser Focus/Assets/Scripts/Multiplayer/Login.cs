using UnityEngine;
//using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class Login : MonoBehaviour
{
    private float width;
    private float height;
    private string debug;
    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);

        GUI.Label(new Rect(20, 20, width, height * 0.25f),
            debug);
        //GUI.Label(new Rect(20, 20, width, height * 0.25f), hitName.ToString());

    }

    public void OnClickButton()
    {
        PlayerConfig.CreateJSON();
        PlayerConfig.Load();
        if (PlayerConfig.GetPlayerID() != -1 && PlayerConfig.GetPlayerUsername() != null)
        {
            debug = "Connecting";
            StartCoroutine(LoginUser(PlayerConfig.GetPlayerID(), PlayerConfig.GetPlayerUsername()));
        }
        else
        {
            ErrorCalled("No Registered User");
            SceneLoader.instance.LoadLevel(1);
        }
    }


    public IEnumerator LoginUser(int ID, string User)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginID", ID);
        form.AddField("loginUser", User);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/laserFocus/phpsqliteconnect/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
                StopAllCoroutines();
                transform.GetComponentInChildren<TMP_Text>().text = www.error;
                transform.GetComponentInChildren<TMP_Text>().color = Color.red;
                StartCoroutine(FadeText(1.0f, 2.0f));
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                int code;
                Debug.Log(jsonArray);
                if (int.TryParse(jsonArray, out code))
                {
                    switch (code)
                    {
                        case 0:
                            //CONNECTION ERROR
                            StopAllCoroutines();
                            transform.GetComponentInChildren<TMP_Text>().text = "Connection Error: Unable to Connect";
                            transform.GetComponentInChildren<TMP_Text>().color = Color.red;
                            StartCoroutine(FadeText(1.0f, 2.0f));
                            break;
                        case 1:
                            //USERNAME NOT FOUND
                            SceneLoader.instance.LoadLevel(1);
                            // LOAD REGISTRATION SCENE
                            break;
                        case 2:
                            //LOGIN
                            SceneLoader.instance.LoadLevel(2);
                            //LOAD GAME SCENE
                            break;
                        default:
                            //UNKNOWN ERROR
                            StopAllCoroutines();
                            transform.GetComponentInChildren<TMP_Text>().text = "ERROR " + jsonArray;
                            transform.GetComponentInChildren<TMP_Text>().color = Color.red;
                            StartCoroutine(FadeText(1.0f, 2.0f));
                            break;
                    }
                }
                else
                {
                    StopAllCoroutines();
                    transform.GetComponentInChildren<TMP_Text>().text = "Connection Error: Unknown Response";
                    transform.GetComponentInChildren<TMP_Text>().color = Color.red;
                    StartCoroutine(FadeText(1.0f, 2.0f));
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
            transform.GetComponentInChildren<TMP_Text>().color = new Color(transform.GetComponentInChildren<TMP_Text>().color.r, transform.GetComponentInChildren<TMP_Text>().color.g, transform.GetComponentInChildren<TMP_Text>().color.b, Mathf.SmoothStep(1.0f, 0.0f, t));
            
            yield return null;
        }

    }

    public void ErrorCalled(string error = "ERROR: Unknown")
    {
        StopAllCoroutines();
        transform.GetComponentInChildren<TMP_Text>().text = error;
        transform.GetComponentInChildren<TMP_Text>().color = Color.red;
        StartCoroutine(FadeText(1.0f, 2.0f));
    }
}
