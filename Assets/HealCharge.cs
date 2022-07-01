using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealCharge : MonoBehaviour
{
    public float healID;
    public Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSingleton.Instance.playerCurrentHealingCharges < healID)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
        }
    }
}
