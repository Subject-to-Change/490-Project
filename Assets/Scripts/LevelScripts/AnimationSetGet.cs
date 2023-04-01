using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetGet : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getAnimBool(string boolName)
    {

        bool returnable = anim.GetBool(boolName);

        return returnable;

    }

    public void setAnimBool(string boolName, bool setter)
    {

       anim.SetBool(boolName, setter);

    }

    public void setAnimTrigger(string triggerName)
    {

        anim.SetTrigger(triggerName);

    }

    public void resetAnimTrigger(string triggerName)
    {

        anim.ResetTrigger(triggerName);

    }

    public void setAnimFloat(string floatName, float value)
    {

        anim.SetFloat(floatName, value);

    }

    public float getAnimFloat(string floatName)
    {

        float returnable = anim.GetFloat(floatName);

        return returnable;

    }


}
