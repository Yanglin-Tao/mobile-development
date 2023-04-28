using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    public void SceneLoader()
    {
        SceneManager.LoadScene(sceneName);
    }
}