using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneUIcontainer : MonoBehaviour
{
    public GameObject loadingImgGroup;
    private Image loadingImage;
    public Slider progressBar;
    public List<string> sceneNames;
    private string loadSceneName;
    public bool isStartScene;
    public bool isLoading;
    private void Awake()
    {
        if (isStartScene)
        {
            for (int i = 0; i < sceneNames.Count; i++)
            {
                MoveSceneData.Instance.SetSceneData(i, sceneNames[i]);
            }

            MoveSceneData.Instance.CurrentScene = "StartScene"; // start Scene에서 배틀씬으로 바로 넘어가지 않게 막기 위한 용도
            TowerManager.Instance.callScripts();
        }

    }

    private void OnDisable()
    {
        // StopCoroutine(loadingBar());
    }
    public void LoadScene(string sceneName)
    {
        if (isLoading) return;
        isLoading = true;
        TowerManager.Instance.ResetAllData();
        if (MoveSceneData.Instance.CurrentScene == "StartScene")
        {
            loadSceneName = "LobbyScene";
        }
        else
        {
            loadSceneName = sceneName;
        }


        int _imgNum = Random.Range(0, loadingImgGroup.transform.childCount);
        loadingImage = loadingImgGroup.transform.GetChild(_imgNum).GetComponent<Image>();
        loadingImage.gameObject.SetActive(true);
        MoveSceneData.Instance.CurrentScene = loadSceneName;
        if (ObjectPool.instance != null)
        {
            ObjectPool.instance.DeActiveAllPref();
        }
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        StartCoroutine(loading(loadSceneName));
    }


    IEnumerator loading(string sceneName)
    {
        Time.timeScale = 1;

        AsyncOperation _operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        loadingImage.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(true);
        while (!_operation.isDone)
        {
            float _progress = Mathf.Clamp01(_operation.progress / 0.9f);

            progressBar.value = _progress;

            if (_operation.isDone)
            {
                Time.timeScale = 0;
            }
            yield return null;
        }
        isLoading = false;
        // if (_operation.isDone) StopCoroutine(loading());

    }
}
