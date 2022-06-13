using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class LookAtMovementFlip : MonoBehaviour
{
    Vector3 dir;
    bool startLookingRight = false;
    Rigidbody2D myRB;
    SpriteRenderer mySR;

    // Start is called before the first frame update
    void Start()
    {
        myRB = this.GetComponent<Rigidbody2D>();
        mySR = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = myRB.velocity;
        
        if(dir.x > 0)
        {
            mySR.flipX = startLookingRight ? false : true;
        }
        else if(dir.x < 0)
        {
            mySR.flipX = startLookingRight ? true : false;
        }
    }
}
