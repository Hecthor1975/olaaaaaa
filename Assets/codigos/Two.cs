using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<One>().message = "Mensaje modificado de dos";
   
       Debug.Log("Mensaje modificado de dos");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
