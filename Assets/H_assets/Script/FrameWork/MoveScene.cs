using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveScene : Singleton<MoveScene>
{
    protected MoveScene() { }
    public GameObject loadingImgGroup;
    public Image loadingImage;
    public Image progressBar;

    private string loadSceneName;

    private void Awake()
    {
        if (loadingImgGroup != null)
        {
            int imgNum = loadingImgGroup.transform.childCount;
            loadingImage = loadingImgGroup.transform.GetChild(Random.Range(0, imgNum)).GetComponent<Image>();
        }

    }
    private void OnDisable()
    {
        StopCoroutine(loadingBar());
    }
    public void LoadScene(string sceneName)
    {
        loadSceneName = sceneName;
        loadingImage.gameObject.SetActive(true);
        StartCoroutine(loadingBar());

    }

    IEnumerator loadingBar()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(loadSceneName);

        yield return new WaitForSeconds(1f);
        loadingImage.gameObject.SetActive(false);
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
}
