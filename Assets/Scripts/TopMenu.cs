using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TopMenu : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI timerTitle;
    [SerializeField] private TextMeshProUGUI gameStateTitle;

    private void Awake()
    {
        retryButton.onClick.AddListener(() =>
        {
            Loader.Load(SceneManager.GetActiveScene().buildIndex);
        });
        nextButton.onClick.AddListener(() =>
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            Debug.Log(nextScene);
            if (nextScene < SceneManager.sceneCountInBuildSettings)
                Loader.Load(nextScene);
            else
                Loader.Load(Loader.Scene.MainMenuScene);
        });
        

    }

    private void Update()
    {
        SetRetryButton();
        SetNextButton();
        SetTimerTitle();
        SetGameStateTitle();
    }

    private void SetRetryButton()
    {
        if (GameHandler.Instance.IsGameOver())
        {
            retryButton.gameObject.SetActive(true);
        }
    }
    private void SetNextButton()
    {
        if (GameHandler.Instance.IsGameOver() && GameHandler.Instance.IsPlayerWon())
        {
            nextButton.gameObject.SetActive(true);
        }
    }
    private void SetGameStateTitle()
    {
        if (GameHandler.Instance.IsGameOver())
        {
            gameStateTitle.transform.parent.gameObject.SetActive(true);
            if (GameHandler.Instance.IsPlayerWon())
                gameStateTitle.text = "Level Completed";
            else 
                gameStateTitle.text = "Time Out";
        }
    }
    public void SetTimerTitle()
    {
        if (Level.Instance.IsTimerOn())
        {
            timerTitle.transform.parent.gameObject.SetActive(true);
            timerTitle.text = Mathf.Ceil(Level.Instance.GetTimer()).ToString();
        }
        else
        {
            timerTitle.transform.parent.gameObject.SetActive(false);
        }
    }
}
