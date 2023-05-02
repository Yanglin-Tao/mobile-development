using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public string sceneName;
    GameManager _gameManager;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        if (_gameManager != null){
            Debug.Log("found game manager");
        }
    }


    public void SceneLoader()
    {
        if (_gameManager != null){
            if (_gameManager.checkUnlock(sceneName)){
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}