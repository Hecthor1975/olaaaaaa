using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGameTimeScale : MonoBehaviour
{
    public float lastChangeTimeScale = 0;

    public void PauseGame()
    {
        // Save the last float value != 0
        if (Time.timeScale != 0)
        {
            lastChangeTimeScale = Time.timeScale;
        }
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = lastChangeTimeScale;
    }

    public void ChangeTimeScale(float newTimeScale)
    {
        lastChangeTimeScale = newTimeScale;
        Time.timeScale = newTimeScale;
    }
}

