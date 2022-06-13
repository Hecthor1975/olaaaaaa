using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float y;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,y,0f);
    }
}
