using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTime : MonoBehaviour
{
    public void ChangeTime(float newTime)
    {
        Time.timeScale = newTime;
    }
}
