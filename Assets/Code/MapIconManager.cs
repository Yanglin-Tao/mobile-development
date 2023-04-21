using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapIconManager : MonoBehaviour
{
    public string chosenScene;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            // load the scene
            SceneManager.LoadScene("ChooseCharacter");

            // set the current scene in game manager to sceneName
            // gameManager.setChosenScene = sceneName;
        }
    }
}
