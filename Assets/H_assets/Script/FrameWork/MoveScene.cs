using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveScene : Singleton<MoveScene>
{
    protected MoveScene() { }
    public Image loadingImage;
    public Image progressBar;

    private string loadSceneName;
    void Start()
    {

    }

    private void OnDisable()
    {
        //StopCoroutine(loadingBar());
    }
    // Update is called once per frame
    void Update()
    {

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
        //gameObject.SetActive(true);
    }
}
