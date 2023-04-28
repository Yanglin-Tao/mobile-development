using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    GameManager _gameManager;

    private void Start() {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update() {
        // get name of current attached game object
        // string name = gameObject.name;
        string tag = gameObject.tag;
        // if name includes "Enemy"
        if (tag == "EnemyHealthBar"){
            // get the enemy's health
            SetSliderValue((int)_gameManager.getEnemyHealth());
        }
        else {
            // get the player's health
            SetSliderValue((int)_gameManager.getHealth());
        }
        // if (name.Contains("Enemy")){
        //     // get the enemy's health
        //     SetSliderValue((int)_gameManager.getEnemyHealth());
        // }
        // else {
        //     // get the player's health
        //     SetSliderValue((int)_gameManager.getHealth());
        // }
    }

    public void InitiateSliderValue(int health){
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetSliderValue(int health){
        slider.value = health;
    }
}
