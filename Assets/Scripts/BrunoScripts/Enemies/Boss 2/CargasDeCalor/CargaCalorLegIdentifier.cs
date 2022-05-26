using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaCalorLegIdentifier : MonoBehaviour
{
    [SerializeField] private LegType legType;
    private GorgonopsiaBoss boss;
    public int health;
    private void Start()
    {
        boss = FindObjectOfType<GorgonopsiaBoss>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            if (health > 1) health--;
            else
            {
                switch (legType)
                {
                    case LegType.left:
                        gameObject.SetActive(false);
                        boss.leftLegOn = false;
                        boss.currentHeatCharges--;
                        break;

                    case LegType.right:
                        gameObject.SetActive(false);
                        boss.rightLegOn = false;
                        boss.currentHeatCharges--;
                        break;
                }
            }
            
        }
    }
}
public enum LegType
{
    left,
    right
}
