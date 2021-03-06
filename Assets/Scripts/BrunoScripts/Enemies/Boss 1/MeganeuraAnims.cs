using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeganeuraAnims : MonoBehaviour
{
    public Animator animator;
    private MeganeuraBoss boss;
    private MeganeuraStats stat;
    
    private void Start()
    {       
        boss = GetComponent<MeganeuraBoss>();
        stat = GetComponent<MeganeuraStats>();
    }
    private void Update()
    {       
        animator.SetBool("onAir",stat.onAir);
        animator.SetBool("isActive", boss.isActive);
        animator.SetBool("isDead", !stat.isAlive);
        animator.SetBool("isActing", stat.isAttacking);
    }
    public void DamageAnimation()
    {
        animator.SetTrigger("damageTrigger");
    }
    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) animator.SetBool("isActive", true);
    }

}
