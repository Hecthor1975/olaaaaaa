using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitForPlayer : MonoBehaviour
{
    public Transform player;
    public float minimalDistance = 10;
    private bool playerIsNear = false;
    public UnityAction onNear;
    public UnityAction onFar;

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) <= minimalDistance)
        {
            if(playerIsNear == false)
            {
                playerIsNear = true;
                onNear?.Invoke();
            }
        }
        else
        {
            if (playerIsNear == true)
            {
                playerIsNear = false;
                onFar?.Invoke();
            }
        }
    }
}
