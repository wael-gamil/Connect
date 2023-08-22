using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance;
    [SerializeField] private Button playButton;
    [SerializeField] private Button levelSelect;
    [SerializeField] private Button quitButton;

    [SerializeField] private Toggle chillToggle;
    [SerializeField] private Toggle mediumToggle;
    [SerializeField] private Toggle extremeToggle;
    private int difficulty = 0;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Level_1);
        });
        levelSelect.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.LevelMenu);
        });
        quitButton.onClick.AddListener(() =>
        {
            SaveSystem.SavePlayer();
            Application.Quit();
        });
        chillToggle.onValueChanged.AddListener((value) => {
            if (value)
                difficulty = 0;
        });
        mediumToggle.onValueChanged.AddListener((value) => {
            if (value)
                difficulty = 1;
        });
        extremeToggle.onValueChanged.AddListener((value) => {
            if (value)
                difficulty = 2;
        });
    }
    public int GetDifficultyValue()
    {
        return difficulty;
    }
}