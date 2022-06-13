using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatinoControllerTree : MonoBehaviour
{
    [SerializeField] Animator animatorOBJ;
    [SerializeField] string AnimationID = "Jump";
    
    public void ChangeAnimation(bool newState)
    {
        animatorOBJ.SetBool(AnimationID, newState);
    }

}
