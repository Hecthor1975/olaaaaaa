using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDependingOnX : MonoBehaviour
{
    
    public Transform target;
    public float speedPerUnit = 1;

    private float actualPositionX;
    private float initialPositionX;

    // Start is called before the first frame update
    void Start()
    {
        initialPositionX = target.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetLastPosition = target.transform.position;
        Vector3 lastPosition = this.transform.position;
        actualPositionX = targetLastPosition.x;
        float newPosX = speedPerUnit * (initialPositionX - actualPositionX) + initialPositionX;
        this.transform.position =
            new Vector3(newPosX, lastPosition.y, lastPosition.z);
    }
}
