using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public List<Sprite> characters =  new List<Sprite>(); // hold different character sprites
    int currentChar = 0; // to keep track of different characters in a scene
    public GameObject characterSprite;
    public SpriteRenderer spriteRenderer;
    private string chosenScene;
    GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        chosenScene = _gameManager.GetChosenScene();
    }
    
    // Back Button
    public void Back() {
        currentChar -= 1; // move back 1 if back button is clicked
        if (currentChar < 0) {
            currentChar = characters.Count - 1; // rotate back
        }
        spriteRenderer.sprite = characters[currentChar]; // set the sprite
    }
    
    // Next Button
    public void Next() {
        currentChar += 1; // move front 1 if next button is clicked
        if (currentChar == characters.Count) {
            currentChar = 0; // rotate back
        }
        spriteRenderer.sprite = characters[currentChar]; // set the sprite
    }

    // Play Button
    public void Play() {
#if UNITY_EDITOR
        PrefabUtility.SaveAsPrefabAsset(characterSprite, "Assets/Prefabs/currentChar.prefab"); // need the actual path 
        Debug.Log("CHARACTER SPRITE SAVED AT INDEX: ");
        Debug.Log(currentChar);
#endif
        SceneManager.LoadScene(chosenScene); // next scene to load after choosing the character
    }
}
