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
    // public int health = 100;
    // public int enemyHealth = 100;
    public int health;
    public int enemyHealth;
    
    // public int maxHealth;
    // public int maxEnemyHealth;
    string levelName;

    private bool GameOver = false;
    public bool enemyKilled = false;
    public GameObject selectedPlayer; // selected player from the choosing scene
    private Sprite currentSprite; // selected sprite loaded as currentSprite for the level
    // public GameObject mainPlayer; // player to be loaded from selected one
    private GameObject mainPlayer;
    public GameObject enemy;
    private string currentChoosenScene;
    private static GameManager instance = null;
    // private static GameManager instance;

    public bool unlockLevel2 = false;
    public bool unlockLevel3 = false;
    public bool unlockLevel4 = false;

    private string currentLevel = "Level1";

    public void unlockLevel(string unlockedLevel){
        if (unlockedLevel == "Level2"){
            unlockLevel2 = true;
        }
        else if (unlockedLevel == "Level3"){
            unlockLevel3 = true;
        }
        else if (unlockedLevel == "Level4"){
            unlockLevel4 = true;
        }
    }

    public bool checkUnlock(string levelName){
        if (levelName == "Level1"){
            return true;
        }
        if (levelName == "Level2"){
            return unlockLevel2;
        }
        else if (levelName == "Level3"){
            return unlockLevel3;
        }
        else if (levelName == "Level4"){
            return unlockLevel4;
        }
        else{
            return false;
        }
    }

    // choose character methods
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
        // Debug.Log("awake");
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // After we do the check
        // Scene scene = SceneManager.GetActiveScene();
        // levelName = scene.name;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start(){
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // private void OnEnable()
    // {
    //     // Register the OnSceneLoaded method to be called when a new scene is loaded
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // private void OnDisable()
    // {
    //     // Unregister the OnSceneLoaded method when this script is disabled or destroyed
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("on scene loaded");
        // currentLevel = SceneManager.GetActiveScene().name;
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        // set maximum player health
        if (mainPlayer != null){
            Player playerScript = mainPlayer.GetComponent<Player>();
            if (playerScript != null) {
                health = playerScript.health;
                // maxHealth = health;
                // Debug.Log("player found with tag");
            }
        }

        // set maximum enemy health
        if (scene.name == "Level1"){
            enemyHealth = 100;
        }
        else if (scene.name == "Level2"){
            enemyHealth = 150;
        }
        else if (scene.name == "Level3"){
            enemyHealth = 100;
        }
        else if (scene.name == "Level4"){
            enemyHealth = 100;
        }
        
        // CHOOSE CHARACTER CODE
        // currentSprite = selectedPlayer.GetComponent<SpriteRenderer>().sprite;
        // if (mainPlayer != null)
        // {
        //     SpriteRenderer spriteRenderer = mainPlayer.GetComponent<SpriteRenderer>();
        //     if (spriteRenderer != null)
        //     {
        //         spriteRenderer.sprite = currentSprite;
        //     }
        // }
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
        if (mainPlayer != null){
            health = amount;
            // player damaged
            // Debug.Log("Set health runs");
            mainPlayer.GetComponent<Animator>().SetBool("Damage", true);
            if (health <= 0){
                GameOver = true;
                health = 0;
            }
            //healthUI.text = "HEALTH: " + health;
        }
    }

    public int getEnemyHealth(){
        return enemyHealth;
    }

    // public void resetHealth() {
    //     health = 100;
    //     enemyHealth = 100;
    //     // Debug.Log("reset health to 100");
    // }

    public void SetEnemyHealth(int newEnemyHealth)
    {
        enemyHealth = newEnemyHealth;
        if (enemy){
            enemy.GetComponent<Animator>().SetBool("Damage", true);
            StartCoroutine(ResetDamageAnimation());
            if (enemyHealth < 0)
            {
                enemyHealth = 0;
                setEnemyKilled(true);
            }
        }

    }

    IEnumerator ResetDamageAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        if (enemy){
            enemy.GetComponent<Animator>().SetBool("Damage", false);
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
            // resetHealth();
            SceneManager.LoadScene("EndFail");
            GameOver = false;
        }
        if (enemyKilled){
            // currentLevel = SceneManager.GetActiveScene().name;
            if (currentLevel == "Level4"){
                // go to EndWin scene
                // resetHealth();
                SceneManager.LoadScene("EndWin");
                enemyKilled = false;
            }
            else{
                // resetHealth();
                if (currentLevel == "Level1"){
                    unlockLevel("Level2");
                    // Debug.Log("Level2 unlocked!");
                    currentLevel = "Level2";
                }
                else if (currentLevel == "Level2"){
                    unlockLevel("Level3");
                    // Debug.Log("Level3 unlocked!");
                    currentLevel = "Level3";
                }
                else if (currentLevel == "Level3"){
                    unlockLevel("Level4");
                    // Debug.Log("Level4 unlocked!");
                    currentLevel = "Level4";
                }
                SceneManager.LoadScene("Map");
                enemyKilled = false;
            }
        }
        screenChecker();
        // Debug.Log(enemyHealth);
        //Debug.Log(health);
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
        // resetHealth();
        SceneManager.LoadScene(level);
    }
}
