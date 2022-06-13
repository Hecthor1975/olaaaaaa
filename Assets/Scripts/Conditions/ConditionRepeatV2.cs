using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConditionRepeatv2 : MonoBehaviour
{
	public float initialDelay = 0f;
	public float frequency = 1f;
	float actualTime = 0;
	public bool useInitialDelay = false;

	public UnityEvent action;

	private void Start()
	{
		actualTime = 0;
        if (useInitialDelay)
        {
			actualTime = frequency - initialDelay;
		}
	}


	private void Update()
	{
		actualTime += Time.deltaTime;
		if (actualTime >= frequency)
		{
			if (action != null) action.Invoke();
			actualTime = 0;
		}
	}
}