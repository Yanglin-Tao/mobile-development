using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapIconManager : MonoBehaviour
{
    public string chosenScene;

    GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCursor")){
            // load the scene
            SceneManager.LoadScene("ChooseCharacter");
            // Debug.Log("chosen scene: ");
            // Debug.Log(chosenScene);
            // set the current scene in game manager to sceneName
            _gameManager.SetChosenScene(chosenScene);
            // Debug.Log("SET CHOSEN SCENE RUNS");
        }
    }
}
