using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitAndDo : MonoBehaviour
{
    [SerializeField] private bool alwaysWorking = false;
    [SerializeField] private float totalWaitTime = 2f;
    private float actualWaitTime = 0;

    public UnityEvent onLoop;
    bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        if (alwaysWorking)
        {
            TurnTimerOn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting)
        {
            actualWaitTime -= Time.deltaTime;

            if(actualWaitTime <= 0)
            {
                actualWaitTime = 0;
                onLoop?.Invoke();
                isWaiting = false;
                if (alwaysWorking)
                {
                    actualWaitTime = totalWaitTime;
                    isWaiting = true;
                }
            }
        }
    }

    public void TurnTimerOn()
    {
        actualWaitTime = totalWaitTime;
        isWaiting = true;
    }
}
