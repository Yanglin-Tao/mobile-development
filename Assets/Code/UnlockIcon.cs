using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockIcon : MonoBehaviour
{
    GameManager _gameManager;
    public string levelName;

    void Start(){
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update(){
        if (_gameManager != null){
            if (_gameManager.checkUnlock(levelName)){
                gameObject.SetActive(false);
            }
            else{
                gameObject.SetActive(true);
            }
        }
    }
}
