using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public GameObject panel;
    public GameObject loadingPanel;
    public TMP_Text errorText;

    public void ShowPanel(bool _value)
    {
        panel.SetActive(_value);
    }

    public void ShowLoading(bool _value)
    {
        loadingPanel.SetActive(_value);
    }

    public void ShowError(float _fadeDuration = 1.0f, float _fadeDelay = 3.0f, string _error_msg = "ERROR:")
    {
        StopAllCoroutines();
        errorText.text = _error_msg;
        errorText.color = Color.red;
        StartCoroutine(FadeText(_fadeDuration, _fadeDelay));
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
