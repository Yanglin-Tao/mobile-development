using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //int score = 0;
    int life = 0;
    string levelName;
    
    // public TMPro.TextMeshProUGUI scoreUI;
    // public TMPro.TextMeshProUGUI lifeUI;

    private bool GameOver = false;
    private bool enemyKilled = false;
    public GameObject selectedPlayer; // selected player from the choosing scene
    private Sprite currentSprite; // selected sprite loaded as currentSprite for the level
    public GameObject mainPlayer; // player to be loaded from selected one
    private string currentChoosenScene;
    private static GameManager instance = null;
    public void SetChosenScene(string choosenScene) {
        currentChoosenScene = choosenScene;
    }
    public string GetChosenScene() {
       Debug.Log("Game Manager GetChosenScene menthod Current Choosen Scene: ");
       Debug.Log(currentChoosenScene);
       return currentChoosenScene;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // After we do the check
        Scene scene = SceneManager.GetActiveScene();
        levelName = scene.name;
    }
    
    void Start()
    {
        // Needed for the choose character scene
        // ------
        currentSprite = selectedPlayer.GetComponent<SpriteRenderer>().sprite;
        mainPlayer.GetComponent<SpriteRenderer>().sprite = currentSprite;
        // ------
        //lifeUI.text = "HEALTH: " + life;
    }

    public void NextScene(string name){
        StartCoroutine(NextScene(5, name));
    }

    public void setEnemyKilled(bool flag){
        enemyKilled = flag;
    }


    public float getHealth(){
        return life;
    }

    // please use SetLife for ADDING and SUBTRACTING life
    // just use negative for the subtracting life
    public void SetLife(int amount)
    {
        life = amount;
        if (life < 0){
            GameOver = true;
            life = 0;
        }
        //lifeUI.text = "HEALTH: " + life;
        
    }

    public string getScene(){
        return levelName;
    }


    void screenChecker()
    {
#if !UNITY_WEBGL
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        if (GameOver){
            //StartCoroutine(NextScene(6, ));
            GameOver = false;
        }
        screenChecker();
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    IEnumerator NextScene(int seconds, string level) {
        int counter = seconds;
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(level);
    }
}
