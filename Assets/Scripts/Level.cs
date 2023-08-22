using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    public static Level Instance;

    [SerializeField]private RotationHandler[] rotationHandlers;
    [SerializeField]private int valid = 0;

    public event EventHandler OnGameOver;
    public event EventHandler OnPlayerWin;
    public event EventHandler OnTimeOut;

    public enum Difficulty
    {
        Chill,
        Medium,
        Extreme,
    }
    private Difficulty difficulty;

    private bool isTimerOn = false;
    private float timer;
    private void Awake()
    {
        Instance = this;

        rotationHandlers = GetComponentsInChildren<RotationHandler>();
        foreach(RotationHandler child in rotationHandlers)
            child.Rotate(Randomize(child.GetRotationStep()));
        SetDifficulty();
    }
    private void Start()
    {
        DifficultyHandler();
    }
    private void Update()
    {
        if (isTimerOn)
            CountDown();
    }
    private void CountDown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            OnGameOver?.Invoke(this, EventArgs.Empty);
            OnTimeOut?.Invoke(this, EventArgs.Empty);

            isTimerOn = false;
        }
    }
    private void DifficultyHandler()
    {
        switch (difficulty)
        {
            case Difficulty.Chill: 
                break;
            case Difficulty.Medium: isTimerOn = true;
                timer = 50f;
                break;
            case Difficulty.Extreme:
                isTimerOn = true;
                timer = 30f; break;
        }
    }
    private int Randomize(int max)
    {
        int x = Random.Range(0, max);
        switch (x)
        {
            case 0: x = 0; break;
            case 1: x = 90; break;
            case 2: x = 180; break;
            case 3: x = 270; break;
        }
        return x;
    }
    public void CheckAllRotation()
    {
        if (valid != rotationHandlers.Length)
        {
            valid = 0;
            foreach (RotationHandler child in rotationHandlers)
                if (child.GetCurrentRotation() == 0)
                    valid++;
            if (valid == rotationHandlers.Length)
            {
                OnGameOver?.Invoke(this, EventArgs.Empty);
                OnPlayerWin?.Invoke(this, EventArgs.Empty);
                isTimerOn = false;
            }
        }
    }
    public void SetDifficulty()
    {
        switch(MainMenuUI.Instance.GetDifficultyValue())
        {
            case 0:
                difficulty = Difficulty.Chill;
                break;
            case 1:
                difficulty = Difficulty.Medium;
                break;
            case 2:
                difficulty = Difficulty.Extreme;
                break;
        }
    }
    public bool IsTimerOn()
    {
        return isTimerOn;
    }
    public float GetTimer()
    {
        return timer;
    }
}
