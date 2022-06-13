using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    public float offsetRotation = 180;

    // Update is called once per frame
    void Update()
    {
       if(target != null)
        {
            Vector3 otherPos = transform.position - target.position;
            float alpha = Mathf.Atan2(otherPos.x, otherPos.y) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0, 0, offsetRotation - alpha);

        } 
    }
}
