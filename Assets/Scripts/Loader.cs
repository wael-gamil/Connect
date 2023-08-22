using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Loader
{
   
    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        LevelMenu,
        Level_1,
    }
    private static Scene targetScene;
    private static int targetSceneInt;
    private static string targetSceneString;
    private static bool isInt = false;
    private static bool isString = false;
    public static void Load(Scene targetScene)
    {
        isInt = false;
        isString = false;
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }
    public static void Load(int targetScene)
    {
        isInt = true;
        targetSceneInt = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }
    public static void Load(string targetScene)
    {
        isString = true;
        targetSceneString = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }
    public static void LoaderCallBack()
    {
        if (isInt)
            SceneManager.LoadScene(targetSceneInt);
        else if (isString)
            SceneManager.LoadScene(targetSceneString);
        else
            SceneManager.LoadScene(targetScene.ToString());
    }
}
