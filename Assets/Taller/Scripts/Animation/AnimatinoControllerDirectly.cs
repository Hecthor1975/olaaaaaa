using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatinoControllerDirectly : MonoBehaviour
{
    [SerializeField] Animator animatorOBJ;
    [SerializeField] string AnimationAnimatorID = "Jump";
    
    public void ChangeAnimation()
    {
        animatorOBJ.Play("Base Layer." + AnimationAnimatorID, 0, 0);
    }

}
