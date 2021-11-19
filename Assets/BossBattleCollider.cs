using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleCollider : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BattleCollider;
    public bool startbattle = false;
    
    
    private void Awake()
    {
        BattleCollider.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && startbattle == false)
        {
            BattleCollider.SetActive(true);
            startbattle = true;
        }
    }
}
