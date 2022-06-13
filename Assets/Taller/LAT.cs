using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This complete script can be attached to a camera to make it
// continuously point at another object.

public class LAT : MonoBehaviour
{
    [SerializeField]
    private Transform realTarget;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private SpriteRenderer weapon;
    [SerializeField]
    private SpriteRenderer player;

    void Update()
    {
        if (target == null)
            target = transform;

        if (mainCamera == null)
            mainCamera = Camera.main;


        Vector3 mouseWorldPosition = realTarget.transform.position;
        mouseWorldPosition.z = 0;

        Vector3 lookAtDirecion = mouseWorldPosition - target.position;
        target.right = lookAtDirecion;

        //Debug.Log(transform.rotation.z);

        if (transform.rotation.z >= -0.69f && transform.rotation.z <= 0.69f)
        {
            //Debug.Log("R");
            weapon.flipY = false;
            player.flipX = false;
        }
        else
        {
            //Debug.Log("L");
            weapon.flipY = true;
            player.flipX = true;
        }
    }
}