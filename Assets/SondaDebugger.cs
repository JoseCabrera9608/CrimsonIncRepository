using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SondaDebugger : MonoBehaviour
{
    public GameObject[] sondas;
    public string input;
    public EnemiPequeñoControlador enemi;
    //public Animator sondaAnim;

    // Start is called before the first frame update
    void Start()
    {
        sondas = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WalkSpeed(string s)
    {

        input = s;
        foreach (GameObject enemy in sondas)
        {
            enemy.GetComponent<EnemiPequeñoControlador>().agente.speed = float.Parse(input);
        }
     

    }

    public void MeleeAttackSpeed(string s)
    {
        input = s;
        foreach (GameObject enemy in sondas)
        {
            enemy.GetComponent<Animator>().SetFloat("MeleeSpeed", float.Parse(input));
        }
        
        //sondaAnim.SetFloat("MeleeSpeed", float.Parse(input));
    }
    public void DisparoAttackSpeed(string s)
    {

        input = s;
        foreach (GameObject enemy in sondas)
        {
            enemy.GetComponent<Animator>().SetFloat("DisparoSpeed", float.Parse(input));
        }
        //sondaAnim.SetFloat("DisparoSpeed", float.Parse(input));
    }
    public void BombaAttackSpeed(string s)
    {

        input = s;
        foreach (GameObject enemy in sondas)
        {
            enemy.GetComponent<Animator>().SetFloat("BombaSpeed", float.Parse(input));
        }
        //sondaAnim.SetFloat("BombaSpeed", float.Parse(input));
    }
}
