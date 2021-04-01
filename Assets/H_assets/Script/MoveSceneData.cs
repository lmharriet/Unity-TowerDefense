using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MoveSceneData : Singleton<MoveSceneData>
{
    protected MoveSceneData() { }
    public Dictionary<int, string> allScene = new Dictionary<int, string>();
    public string currentScene;

    public void SetSceneData(int sceneNumber, string sceneName)
    {
        allScene[sceneNumber] = sceneName;
    }
    public string GetSceneName(int sceneNumber)
    {
        return allScene[sceneNumber];
    }

    public string CurrentScene
    {
        get { return currentScene; }
        set { currentScene = value; }
    }
    
    public string GetRandomBattleScene()
    {
        return allScene[Random.Range(2, allScene.Count)];
    }
}