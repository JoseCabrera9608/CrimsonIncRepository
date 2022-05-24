using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonopsiaBoss : MonoBehaviour
{
    [SerializeField] public Gstates currentAction;
    private GorgonopsiaStats stats;
    [HideInInspector]public float currentDamageValue;
    void Start()
    {
        stats = GetComponent<GorgonopsiaStats>();
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        currentDamageValue = stats.gorgoDamages[currentAction];
    }
    private void StateMachine()
    {
        switch (currentAction)
        {
            case Gstates.idle:
                break;

            case Gstates.cargaCalor:
                break;

            case Gstates.embestidaFrenetica:
                break;

            case Gstates.blink:
                break;

            case Gstates.rugidoExplosivo:
                break;

            case Gstates.bomba360:
                break;

            case Gstates.alientoCalor:
                break;

            case Gstates.bombaJaeger:
                break;          
        }
    }

    public void HandleAlientoDeCalor()
    {

    }
}
