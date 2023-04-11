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

    public TMPro.TextMeshProUGUI scoreUI;
    public TMPro.TextMeshProUGUI lifeUI;

    private bool GameOver = false;
    private bool enemyKilled = false;

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        levelName = scene.name;
    }

    void Start()
    {
        lifeUI.text = "HEALTH: " + life;
    }

    public void NextScene(string name){
        StartCoroutine(Next(5, name));
    }

    public void setEnemyKilled(bool flag){
        enemyKilled = flag;
    }


    public float getHealth(){
        return life;
    }

    // please use change life for ADDING and SUBTRACTING life
    // just use negative for the subtracting life
    public void ChangeLife(int amount)
    {
        life += amount;
        if (life < 0){
            GameOver = true;
            life = 0;
        }
        lifeUI.text = "HEALTH: " + life;
        
    }

    public int getLife(){
        return life;
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
            StartCoroutine(swapToLost(6));
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



    IEnumerator swapToEnd (int seconds) {
        int counter = seconds;
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("SuccessEnd");
    }
    IEnumerator swapToLost (int seconds) {
        int counter = seconds;
        yield return new WaitForSeconds(seconds);
        score = 0;
        life = 3;
        if (levelName == "Level1"){
            SceneManager.LoadScene("FailEnd1");
        }
        else if (levelName == "Level2"){
            SceneManager.LoadScene("FailEnd2");
        }
        else if (levelName == "Level3"){
            SceneManager.LoadScene("FailEnd3");
        }
        else if (levelName == "Level4"){
            SceneManager.LoadScene("FailEnd4");
        }
    }
    IEnumerator Next(int seconds, string level) {
        int counter = seconds;
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(level);
    }
}
