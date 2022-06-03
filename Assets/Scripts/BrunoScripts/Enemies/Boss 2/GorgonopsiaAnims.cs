using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GorgonopsiaAnims : MonoBehaviour
{
    public Animator animator;
    private GorgonopsiaBoss logicScript;
    private GorgonopsiaStats stats;
   
    void Start()
    {
        //animator = GetComponent<Animator>();
        logicScript = GetComponent<GorgonopsiaBoss>();
        stats = GetComponent<GorgonopsiaStats>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isActive", stats.isActive);
        animator.SetBool("isAlive", stats.isAlive);
        animator.SetBool("isActing", logicScript.isActing);
    }
    public void SetAnimationTrigger(string triggerName)
    {
        //Debug.Log("Trigger set is= " + triggerName);
        animator.SetTrigger(triggerName);
    }
}
