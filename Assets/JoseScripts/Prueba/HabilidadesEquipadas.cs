using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesEquipadas : MonoBehaviour
{
    public Habilidad_SO[] ability;
    [SerializeField] float cooldownTime;
    public float activeTime;
    GameObject player;
    public float distance;
    [SerializeField] private bool casting = false;
    int index;
    public bool attacking = false;
   

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    enum AbilityState
    {
        ready,
        active,
        cooldown
        
    }
    AbilityState state = AbilityState.ready;
    
    
    void Update()
    {
        index = Random.Range(0,ability.Length);
        distance = Vector3.Distance(transform.position, player.transform.position);
        switch (state)
        {
            case AbilityState.ready:
                if (distance <= ability[index].distanceToActivate && distance>= ability[index].minDistance && casting == false) { 
                ability[index].Activate(gameObject);
                state = AbilityState.active;
                activeTime = ability[index].activeTime;
                 }
            break;
            case AbilityState.active:
                if (activeTime > 0 /* && attacking == false */) // && restmode == false
                {

                    casting = true;                   
                    activeTime -= Time.deltaTime;
                    //attacking = true;
                    
                }
                else
                {
                    state = AbilityState.cooldown;
                    cooldownTime = ability[index].cooldownTime;
                    //resttime = 0;
                    //casting = false;
                }
            break;
            case AbilityState.cooldown:
                if (cooldownTime > 0 )
                {
                  cooldownTime -= Time.deltaTime;
                }
                else
                {
                    //attacking = false;
                    //restmode = true; que empiece la cuenta de tiempo ga
                    //resttime += Time.deltaTime;
                    casting = false;
                    state = AbilityState.ready;
                }
                break;
        }
    }
}
