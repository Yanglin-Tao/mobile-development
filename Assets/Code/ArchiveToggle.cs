using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArchiveToggle : MonoBehaviour
{
    public Toggle archiveToggle; // Reference to the Toggle UI element
    GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        archiveToggle.onValueChanged.AddListener(OnToggleValueChanged); // Register a listener for when the toggle value changes
    }

    // enter the archive mode
    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            // unlock all levels
            _gameManager.unlockLevel("Level2");
            _gameManager.unlockLevel("Level3");
            _gameManager.unlockLevel("Level4");
        }
        else{
            // lock all levels again
            _gameManager.lockLevel("Level2");
            _gameManager.lockLevel("Level3");
            _gameManager.lockLevel("Level4");
        }
    }
}
