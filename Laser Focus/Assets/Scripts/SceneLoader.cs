using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class SceneLoader : MonoBehaviour
{

    public static SceneLoader instance;

    public void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    public GameObject loadingScreen;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    public void CONNECTIONERROR(string error = "ERROR: Unknown")
    {
        StartCoroutine(LoadStartWithError(error));
    }

    public void ShowLoadingScreen(bool value)
    {
        loadingScreen.SetActive(value);
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadStartWithError(string error)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
        
        FindObjectOfType<Login>().ErrorCalled();
        
    }
}
