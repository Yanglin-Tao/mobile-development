using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Update() {
        // Testing only
        // if space key is pressed, add one to slider's value
        if (Input.GetKeyDown(KeyCode.Space)){
            // subtract slider value by random value from 1 to 30
            slider.value -= Random.Range(1, 30);
            Debug.Log(slider.value);
        }
    }

    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health){
        slider.value = health;
    }
}
