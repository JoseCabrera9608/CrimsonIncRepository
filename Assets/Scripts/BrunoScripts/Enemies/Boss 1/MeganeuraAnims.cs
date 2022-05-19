using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeganeuraAnims : MonoBehaviour
{
    private Animator animator;
    private MeganeuraBoss boss;
    private MeganeuraStats stat;
    private void Start()
    {
        animator = GetComponent<Animator>();
        boss = GetComponent<MeganeuraBoss>();
        stat = GetComponent<MeganeuraStats>();
    }
    private void Update()
    {       
        animator.SetBool("onAir",stat.onAir);       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) animator.SetBool("isActive", true);
    }

}
