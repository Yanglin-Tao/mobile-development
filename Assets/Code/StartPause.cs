using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPause : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip startSound;

    void Start()
    {
        Time.timeScale = 0;
        // Debug.Log("game paused");
        _audioSource = GetComponent<AudioSource>();
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame ()
    {
        Time.timeScale = 1;
        // Debug.Log("game resumed");
        _audioSource.PlayOneShot(startSound);
    }

    public void QuitGame(){
        // Quit the application
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            UnityEngine.Application.Quit();
        #endif
    }
}
