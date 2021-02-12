using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class InGameHUD : MonoBehaviour
{

    public static InGameHUD instance;

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
    private Transform deckInterfaceParent;

    [SerializeField]
    private GameObject deckPanel;
    [SerializeField]
    private GameObject playerControlsPanel;

    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TMP_Text gameOverText;
    [SerializeField]
    private TMP_Text gameOverMsg;

    [SerializeField]
    private TMP_Text errorText;

    [SerializeField]
    private GameObject optionsPanel;

    private Coroutine errorTextCoroutine;
    private bool isCoroutineRunning = false;

    void Start()
    {
        foreach (Transform child in deckInterfaceParent)
        {
            if (child.GetChild(0).GetComponentInChildren<Image>())
            {
                child.GetChild(0).GetComponentInChildren<Image>().sprite = GameManager.instance.GetPlayerDeck().GetTower(child.GetSiblingIndex()).GetSprite();
                child.GetChild(0).GetComponentInChildren<Image>().color = GameManager.instance.GetPlayerDeck().GetTower(child.GetSiblingIndex()).GetColor();
            }
        }
    }

    public void ShowGameOverPanel(Winner winner)
    {
        switch (winner)
        {
            case Winner.NONE:
                break;
            case Winner.TIE:
                ShowGameOver("TIE", "Both towers where hit on the same turn");
                break;
            case Winner.PLAYER1:
                if (GameManager.player == PlayerID.LOCALPLAYER)
                {
                    ShowGameOver("YOU WIN", "Laser hit oponnents tower");
                }
                else
                {
                    ShowGameOver("YOU LOSE", "Laser hit your tower");

                }
                break;
            case Winner.PLAYER2:
                if (GameManager.player == PlayerID.PLAYER)
                {
                    ShowGameOver("YOU WIN", "Laser hit oponnents tower");
                }
                else
                {
                    ShowGameOver("YOU LOSE", "Laser hit your tower");
                }
                break;
            default:
                break;
        }
    }

    public void ShowGameOver(string _state, string _msg)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = _state;
        gameOverMsg.text = _msg;
        gameOverPanel.GetComponentInChildren<Button>().onClick.AddListener(() => SceneLoader.instance.LoadLevel(2));
    }

    public void CallError(string _msg = "Unknown", float fadeDuration = 1.0f, float fadeStartDelay = 3.0f)
    {
        if (isCoroutineRunning)
        {
            StopCoroutine(errorTextCoroutine);
        }
        errorText.text = _msg;
        errorText.color = Color.red;
        errorTextCoroutine = StartCoroutine(FadeText(fadeDuration, fadeStartDelay));
    }

    IEnumerator FadeText(float fadeDuration, float fadeStartDelay)
    {
        isCoroutineRunning = true;
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
        isCoroutineRunning = false;
    }

    public void StartPlacing()
    {
        playerControlsPanel.SetActive(true);
        deckPanel.SetActive(false);
    }

    public void StopPlacing()
    {
        playerControlsPanel.SetActive(false);
        deckPanel.SetActive(true);
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void HideOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void Forfeit()
    {
        ClientSend.PlayerForfeited();
    }
}
