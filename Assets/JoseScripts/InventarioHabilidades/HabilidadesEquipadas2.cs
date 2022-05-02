using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesEquipadas2 : MonoBehaviour
{
    public Animator anim;
    public int attackIndex;
    public bool activarPoder;
    public int indexDos;
    public GameObject esferaMagnetica;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
       /* index = Random.Range(1, 3);
        attackIndex = index;*/
         if(activarPoder == true)
         {
             StartCoroutine(EmpezarPoder());
             activarPoder = false;
         }
    }

   IEnumerator EmpezarPoder()
    {
        //esferaMagnetica.SetActive(true);
        anim.SetTrigger("Magnetizar");
        indexDos = Random.Range(1, 4);
        attackIndex = indexDos;
        yield return new WaitForSeconds(1);
        anim.SetInteger("AttackIndex", attackIndex);
        
    }

    public void ActivarSphera()
    {
        esferaMagnetica.SetActive(true);
    }
}
