using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public Animation anim;
    public bool playAtStart;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animation>();

        if (playAtStart == true)
        {
            PlayAnimation();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation()
    {
        anim.Play();
    }
}
