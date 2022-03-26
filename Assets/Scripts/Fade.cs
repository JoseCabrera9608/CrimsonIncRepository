using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anim;
    public bool fadeinend;
    public bool fadeoutend;
    public float timer;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Start", true);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.7f)
        {
            anim.SetBool("Start", false);
        }
    }

    public void Fadein()
    {
        anim.SetBool("End", true);
    }

    public void FadeEnd()
    {
        
        fadeinend = true;
    }
}
