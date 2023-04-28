using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //int score = 0;
    // player health
    int health = 100;
    int enemyHealth = 100;
    string levelName;

    // public TMPro.TextMeshProUGUI scoreUI;
    // public TMPro.TextMeshProUGUI healthUI;

    private bool GameOver = false;
    private bool enemyKilled = false;
    public GameObject selectedPlayer; // selected player from the choosing scene
    private Sprite currentSprite; // selected sprite loaded as currentSprite for the level
    // public GameObject mainPlayer; // player to be loaded from selected one
    private GameObject mainPlayer;
    private string currentChoosenScene;
    private static GameManager instance = null;


    public void SetChosenScene(string choosenScene) {
        currentChoosenScene = choosenScene;
    }
    public string GetChosenScene() {
    //    Debug.Log("Game Manager GetChosenScene menthod Current Choosen Scene: ");
    //    Debug.Log(currentChoosenScene);
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // Needed for the choose character scene
        // ------
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        currentSprite = selectedPlayer.GetComponent<SpriteRenderer>().sprite;
        if (mainPlayer != null)
        {
            SpriteRenderer spriteRenderer = mainPlayer.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = currentSprite;
            }
        }
        // ------
        //healthUI.text = "HEALTH: " + health;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        currentSprite = selectedPlayer.GetComponent<SpriteRenderer>().sprite;
        if (mainPlayer != null)
        {
            SpriteRenderer spriteRenderer = mainPlayer.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = currentSprite;
            }
        }
    }

    public void NextScene(string name){
        StartCoroutine(NextScene(5, name));
    }

    public void setEnemyKilled(bool flag){
        enemyKilled = flag;
    }


    public float getHealth(){
        return health;
    }

    // please use SetHealth for ADDING and SUBTRACTING health
    // just use negative for the subtracting health
    public void SetHealth(int amount)
    {
        health = amount;
        if (health < 0){
            GameOver = true;
            health = 0;
        }
        //healthUI.text = "HEALTH: " + health;

    }

    public int getEnemyHealth(){
        return enemyHealth;
    }

    public void SetEnemyHealth(int newEnemyHealth)
    {
        enemyHealth = newEnemyHealth;
        if (enemyHealth < 0){
            enemyHealth = 0;
        }
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
            // go to EndFail scene
            SceneManager.LoadScene("EndFail");
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
