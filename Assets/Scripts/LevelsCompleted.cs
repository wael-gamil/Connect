using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelsCompleted : MonoBehaviour
{
    public static LevelsCompleted Instance;

    [SerializeField]private List<bool> levelsCompletd;
    void Awake()
    {
        for(int i = 3; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Debug.Log(i);
            levelsCompletd.Add(false);
        }
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
            levelsCompletd = data.levelsCompleted;
    }
    public void IncreaseLevelsCompleted(int level)
    {
        levelsCompletd[level - 3] = true;
    }
    public List<bool> GetLevelsCompleted()
    {
        return levelsCompletd;
    }
}
