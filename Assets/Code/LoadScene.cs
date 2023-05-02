using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public string sceneName;
    GameManager _gameManager;

    // void Update(){
    //     _gameManager = GameObject.FindObjectOfType<GameManager>();
    // }

    // void Awake(){
    //     _gameManager = GameObject.FindObjectOfType<GameManager>();
    // }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        if (_gameManager != null){
            Debug.Log("found game manager");
        }
    }

    public void SceneLoader()
    {
        if (_gameManager != null){
            Debug.Log(_gameManager.checkUnlock(sceneName));
            if (_gameManager.checkUnlock(sceneName)){
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}