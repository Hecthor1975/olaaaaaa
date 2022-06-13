using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTimePassed : MonoBehaviour
{
    public float timePassed = 1;
    float actualTimePassed = 0;
    bool isActive = false;
    public UnityAction onTimePassed;

    // Update is called once per frame
    void Update()
    {
        PassTime();
    }

    private void PassTime()
    {
        if(isActive && actualTimePassed >= 0)
        {
            actualTimePassed -= Time.deltaTime;

            if(actualTimePassed <= 0)
            {
                onTimePassed?.Invoke();
                actualTimePassed = 0;
                isActive = false;
            }
        }
    }

    public void DoActionIn(float time)
    {
        timePassed = time;
        actualTimePassed = time;
        isActive = true;
    }
}
