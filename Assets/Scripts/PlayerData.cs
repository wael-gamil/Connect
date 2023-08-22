using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<bool> levelsCompleted;

    public PlayerData()
    {
        this.levelsCompleted = LevelsCompleted.Instance.GetLevelsCompleted();
    }
}
