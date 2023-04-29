using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPause : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
        // Debug.Log("game paused");
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame ()
    {
        Time.timeScale = 1;
        // Debug.Log("game resumed");
    }
}
