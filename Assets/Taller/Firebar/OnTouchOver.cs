using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class OnTouchOver : MonoBehaviour
{
    public LayerMask playerMask;
    public UnityEvent onTouchOver;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfPlayer(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckIfPlayer(collision.gameObject);
    }

    private void CheckIfPlayer(GameObject go)
    {
        if(playerMask == (playerMask | (1 << go.layer)))
        {
            onTouchOver?.Invoke();
        }
    }


}
