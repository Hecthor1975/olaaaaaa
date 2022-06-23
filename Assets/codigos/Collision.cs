using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Chocaste con la moneda");
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Estas dentro de la moneda");
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Saliste de la moneda");
    }
}
