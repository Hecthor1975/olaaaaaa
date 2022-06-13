using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomWaitAndAnim : MonoBehaviour
{
    public enum RandomState
    {
        RandomWait,
        AnimationWait
    }

    [Header("Random Wait")]
    public float randomMinValue;
    public float randomMaxValue;
    float actualRandomValue;
    public UnityEvent onRandomWait;

    [Header("Animation Wait")]
    public float animationTime;
    public UnityEvent onAnimationWait;
    float actualAnimationValue;

    RandomState actualState = RandomState.RandomWait;

    void Update()
    {
        if (actualState == RandomState.RandomWait) RandomWaitUpdate();
        if (actualState == RandomState.AnimationWait) AnimationWaitUpdate();
    }

    private void RandomWaitUpdate()
    {
        if (actualRandomValue == 0) actualRandomValue = Random.Range(randomMinValue, randomMaxValue);
        actualRandomValue -= Time.deltaTime;
        if (actualRandomValue <= 0)
        {
            actualRandomValue = 0;
            actualState = RandomState.AnimationWait;
            onRandomWait?.Invoke();
        }
    }

    private void AnimationWaitUpdate()
    {
        if (actualAnimationValue == 0) actualAnimationValue = animationTime;
        actualAnimationValue -= Time.deltaTime;
        if (actualAnimationValue <= 0)
        {
            actualAnimationValue = 0;
            actualState = RandomState.RandomWait;
            onAnimationWait?.Invoke();
        }
    }
}
