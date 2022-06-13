using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCharacterAndObjectShooter : MonoBehaviour
{
    [System.Serializable]
    public class MoveKeys
    {
        public KeyCode leftKeyCode = KeyCode.LeftArrow;
        public KeyCode rightKeyCode = KeyCode.RightArrow;
    }

    public MoveKeys moveKeys;
    public List<ObjectShooter> shooters = new List<ObjectShooter>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(moveKeys.leftKeyCode))
        {
            FlipL();
        }
        else if(Input.GetKey(moveKeys.rightKeyCode))
        {
            FlipR();
        }
    }

    private void FlipL()
    {
        Vector3 actualScale = this.transform.localScale;
        this.transform.localScale = new Vector3(-(Mathf.Abs(actualScale.x)), actualScale.y, actualScale.z);

        for (int i = 0, length = shooters.Count; i < length; i++)
        {
            Vector3 actualShooterScale  = shooters[i].shootDirection;
            shooters[i].shootDirection = new Vector3(-(Mathf.Abs(actualShooterScale.x)), actualShooterScale.y, actualShooterScale.z);
        }
    }

    private void FlipR()
    {
        Vector3 actualScale = this.transform.localScale;
        this.transform.localScale = new Vector3((Mathf.Abs(actualScale.x)), actualScale.y, actualScale.z);

        for (int i = 0, length = shooters.Count; i < length; i++)
        {
            Vector3 actualShooterScale = shooters[i].shootDirection;
            shooters[i].shootDirection= new Vector3((Mathf.Abs(actualShooterScale.x)), actualShooterScale.y, actualShooterScale.z);
        }
    }
}
