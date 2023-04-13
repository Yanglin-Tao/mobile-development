using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;

public class charControl : MonoBehaviour
{
    public SpriteRenderer charRenderer;
    public List<Sprite> characters =  new List<Sprite>();
    int currentChar = 0;
    public GameObject character;

    // Back Button
    public void Back() {
        currentChar -= 1;
        if (currentChar < 0) {
            currentChar = characters.Count - 1;
        }
        charRenderer.sprite = characters[currentChar];
    }

    // Next Button
    public void Next() {
        currentChar += 1;
        if (currentChar == characters.Count) {
            currentChar = 0;
        }
        charRenderer.sprite = characters[currentChar];
    }

    // Play Button
    public void Play() {
        PrefabUtility.SaveAsPrefabAsset(character, "Assets/Prefabs/name.prefab"); // need the actual path 
        SceneManager.LoadScene("Level1"); // next scene to load after choosing the character
    }
}
