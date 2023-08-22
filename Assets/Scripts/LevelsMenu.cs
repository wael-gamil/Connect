using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelsMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private List<GameObject> level;
    [SerializeField] private List<Button> levelButton;
    private float levelSpacing = 200;
    private int buttonsPerRow = 6;
    private float minX = 50.0f; // Minimum X position
    private float maxX = 1650.0f; // Maximum X position
    private float minY = -50.0f; // Minimum Y position
    private float maxY = -400.0f; // Maximum Y position
    [SerializeField] private Transform parentUI;
    [SerializeField] private List<bool> levelsCompleted;


    [SerializeField] private Button goBackButton;
    private void Start()
    {
        int numLevels = 12;
        int maxButtonsPerRow = Mathf.Min(buttonsPerRow, numLevels);
        int numRows = Mathf.CeilToInt((float)numLevels / maxButtonsPerRow); // calculate the number of rows
        float totalWidth = maxButtonsPerRow * (levelPrefab.GetComponent<RectTransform>().rect.width + levelSpacing) - levelSpacing;
        float startX = (parentUI.GetComponent<RectTransform>().rect.width - totalWidth) / 2.0f;
        levelsCompleted = LevelsCompleted.Instance.GetLevelsCompleted();

        for (int i = 1; i <= SceneManager.sceneCountInBuildSettings-3; i++)
        {
            level.Add(Instantiate(levelPrefab, parentUI));
            // Customize the level button's appearance and behavior

            // Calculate the position of the level button based on its index and spacing
            int buttonIndex = i - 1;
            int rowIndex = numRows - 1 - buttonIndex / maxButtonsPerRow; // reverse the row indices
            int columnIndex = maxButtonsPerRow - 1 - buttonIndex % maxButtonsPerRow; // reverse the column indices
            float xPos = startX + (maxButtonsPerRow - 1 - columnIndex) * (levelPrefab.GetComponent<RectTransform>().rect.width + levelSpacing); // reverse the X positions
            float yPos = -rowIndex * (levelPrefab.GetComponent<RectTransform>().rect.height + levelSpacing);
            xPos = Mathf.Clamp(xPos, minX, maxX);
            yPos = Mathf.Clamp(yPos, minY, maxY);
            level[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

            // Check if the level button is outside the parent element
            if (xPos < minX)
            {
                columnIndex = 0;
                xPos = startX + (maxButtonsPerRow - 1 - columnIndex) * (levelPrefab.GetComponent<RectTransform>().rect.width + levelSpacing);
                rowIndex--;
                yPos = -rowIndex * (levelPrefab.GetComponent<RectTransform>().rect.height + levelSpacing);
                xPos = Mathf.Clamp(xPos, minX, maxX);
                yPos = Mathf.Clamp(yPos, minY, maxY);
                level[buttonIndex].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            }
            level[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().text = '#' + i.ToString();

            levelButton.Add(level[buttonIndex].GetComponent<Button>());

            if (buttonIndex == 0 || levelsCompleted[buttonIndex-1])
            {
                levelButton[buttonIndex].onClick.AddListener(() =>
                {
                int index = buttonIndex + 1;
                int numberOfNonLevelScene = 3 - 1;
                if (index + numberOfNonLevelScene < SceneManager.sceneCountInBuildSettings)
                {
                    string level = "Level_" + index.ToString();
                    Loader.Load(level);
                }
                else
                    Loader.Load(Loader.Scene.MainMenuScene);

                });
            }
            else
            {
                level[buttonIndex].GetComponent<Image>().color = Color.gray;
            }
           
        }

        goBackButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }
}
