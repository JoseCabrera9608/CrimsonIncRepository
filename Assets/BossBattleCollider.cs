using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleCollider : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BattleCollider;
    public bool startbattle = false;
    public ProgressManager progress;
    
    
    private void Awake()
    {
        BattleCollider.SetActive(false);
    }

    void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && startbattle == false)
        {
            //progress.lastCheckpointPos = transform.position;
            BattleCollider.SetActive(true);
            startbattle = true;
        }
    }
}
