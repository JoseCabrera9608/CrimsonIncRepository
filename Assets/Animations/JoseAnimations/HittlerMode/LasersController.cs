using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasersController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator puertaAnimator;
    public GameObject puerta;
    public bool activar;
    public GameObject gas;
    void Start()
    {
        puertaAnimator = puerta.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activar == true)
        {
            StartCoroutine(ActivarHumo());
            activar = false;
        }
    }

  
    IEnumerator ActivarHumo()
    {
        puertaAnimator.SetTrigger("ActivarPuerta");
        gas.SetActive(true);
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);

    }
}
