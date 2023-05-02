using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    GameManager _gameManager;

    void Start(){
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }


    public void SceneLoader()
    {
        if (_gameManager.checkUnlock(sceneName)){
            SceneManager.LoadScene(sceneName);
        }
    }
}