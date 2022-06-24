using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgniAnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject meleeCollider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MeleeActivate()
    {
        meleeCollider.SetActive(true);
    }
    public void MeleeDeactivate()
    {
        meleeCollider.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus.damagePlayer?.Invoke(50);
        }
    }
}
