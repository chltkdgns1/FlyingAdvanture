using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoSingleTon<LoadSceneManager>
{
    bool loading = false;
    protected override void Init()
    {

    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Start LoadScene");
        if (loading) return;

        loading = true;
        LoadingUI.Instance.isActive = true;
        Debug.Log("Ready Start LoadScene");
        StartCoroutine(LoadingScene(sceneName));
    }

    IEnumerator LoadingScene(string sceneName)
    {
        Debug.Log("Loading Scene " + sceneName);
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while (!op.isDone)
        {
            yield return null;          
        }
        loading = false;
        LoadingUI.Instance.isActive = false;
    }
}
