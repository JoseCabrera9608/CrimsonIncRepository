using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CangrejoShowroom : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anim;
    public int indexCrab;
    
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Index", indexCrab);
    }
}
