using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamagePlayer : MonoBehaviour
{
    public int da�oDeArma;
    public int da�oDeArmaPasiva;
    public bool hitted;
    public GameObject efecto;
    public GameObject puntadeLanza;

   

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("CuerpoBoss"))
        {  
            hitted = true;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, puntadeLanza.transform.localPosition);
            Instantiate(efecto, puntadeLanza.transform.position, rotation);
        }
       
    }

   

}
