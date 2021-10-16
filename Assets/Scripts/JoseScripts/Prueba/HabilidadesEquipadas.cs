using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesEquipadas : MonoBehaviour
{
    public Habilidad_SO[] ability;
    [SerializeField] float cooldownTime;
    [SerializeField] float activeTime;
    GameObject player;
    [SerializeField] private float distance;
    [SerializeField] private bool casting = false;
    int index;

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
                if (distance <= ability[index].distanceToActivate && casting == false) { 
                ability[index].Activate(gameObject);
                state = AbilityState.active;
                activeTime = ability[index].activeTime;
                 }
            break;
            case AbilityState.active:
                if (activeTime > 0)
                {
                    casting = true;
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.cooldown;
                    cooldownTime = ability[index].cooldownTime;
                    casting = false;
                }
            break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                  cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }
}
