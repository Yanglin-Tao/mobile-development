using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;

public class charControl : MonoBehaviour
{
    int currentChar = 0;
    public List<Sprite> characters =  new List<Sprite>(); // hold different character sprites
    public GameObject sprite1;
    public SpriteRenderer _renderer;
    
    // Back Button
    public void Back() {
        currentChar -= 1;
        if (currentChar < 0) {
            currentChar = characters.Count - 1;
        }
        _renderer.sprite = characters[currentChar];
    }
    
    // Next Button
    public void Next() {
        currentChar += 1;
        if (currentChar == characters.Count) {
            currentChar = 0;
        }
        _renderer.sprite = characters[currentChar];
    }

    // Play Button
    public void Play() {
        PrefabUtility.SaveAsPrefabAsset(sprite1, "Assets/Prefabs/currentChar.prefab"); // need the actual path 
        SceneManager.LoadScene("Level1"); // next scene to load after choosing the character
    }
}
