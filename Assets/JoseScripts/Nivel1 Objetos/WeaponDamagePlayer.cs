using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamagePlayer : MonoBehaviour
{
    public int daņoDeArma;
    public int daņoDeArmaPasiva;
    public bool hitted;

   

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("CuerpoBoss"))
        {  
            hitted = true;
        }
       
    }

   

}
