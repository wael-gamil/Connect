using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }
    [SerializeField] private AudioClip levelCompleted;
    [SerializeField] private AudioClip timeOut;
    private enum GameState
    {
        Playing,
        GameOver,
    }
    private GameState state;

    private bool isPlayerWon = false;

    private void Awake()
    {
        Instance = this;
        
        Level.Instance.OnGameOver += Level_OnGameOver;
        Level.Instance.OnPlayerWin += Level_OnPlayerWin;
        Level.Instance.OnTimeOut += Level_OnTimeOut;
        state = GameState.Playing;
    }

    private void Level_OnTimeOut(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(timeOut, transform.position, 1f);
    }

    private void Level_OnPlayerWin(object sender, System.EventArgs e)
    {
        isPlayerWon = true;
        AudioSource.PlayClipAtPoint(levelCompleted, transform.position, 1f);
        LevelsCompleted.Instance.IncreaseLevelsCompleted(SceneManager.GetActiveScene().buildIndex);
    }
    private void Level_OnGameOver(object sender, System.EventArgs e)
    {
        state = GameState.GameOver;
    }

    public bool IsPlaying()
    {
        return state == GameState.Playing;
    }
    public bool IsGameOver()
    {
        return state == GameState.GameOver;
    }
    public bool IsPlayerWon()
    {
        return isPlayerWon;
    }
}
