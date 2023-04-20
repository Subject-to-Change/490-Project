using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{

    public static class animationNames {  //TODO: This would be better as an enum and an array of strings for efficiency in comparisons, but using string constants is faster for prototyping
        public const string RUNNING = "SetMove";
        public const string IDLE = "SetIdle";
        public const string JUMP = "SetJump";
        public const string FALL = "SetFalling";
        public const string CROUCH = "SetCrouch";
        public const string CROUCH_RUN = "SetCrouchMove";
        public const string SLIDE = "SetSlide";
    }

    private string activeAnimationTrigger = "";
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        if(!animator) Debug.LogError("PlayerAnimationController script failed to find Animator component!");
        activeAnimationTrigger = animationNames.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMovementAnimation(string animation) {
        
        if(animation.Equals(activeAnimationTrigger)){
            return;
        }

        //Debug.Log(animation);

        if(animation.Equals(animationNames.JUMP)) {
            animator.ResetTrigger(activeAnimationTrigger);
            activeAnimationTrigger = animationNames.FALL;
            animator.SetTrigger(animationNames.JUMP);
            return;
        }

        animator.ResetTrigger(activeAnimationTrigger);
        activeAnimationTrigger = animation;
        animator.SetTrigger(activeAnimationTrigger);

    }
}
