using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapIconManager : MonoBehaviour
{
    public string sceneName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            // load the scene
            Debug.Log("collide");
            SceneManager.LoadScene(sceneName);
        }
    }
}
