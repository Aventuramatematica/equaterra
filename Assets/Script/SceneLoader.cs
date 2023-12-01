using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public float delayInSeconds = 5f;

    void Start()
    {
        Invoke("LoadNextScene", delayInSeconds);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void MinhaFuncao()
    {
        Debug.Log("A função foi chamada!");
        SceneManager.LoadScene(sceneToLoad);
    }
}

