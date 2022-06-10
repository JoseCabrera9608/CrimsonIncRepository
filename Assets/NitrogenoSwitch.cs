using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitrogenoSwitch : MonoBehaviour
{
    public AgniBoss agniBoss;
    public GameObject fire;
    public GameObject ice;

    // Start is called before the first frame update
    void Start()
    {
        agniBoss = GetComponentInParent<AgniBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agniBoss.modoNitrogeno == true)
        {
            ice.SetActive(true);
            fire.SetActive(false);
        }
        else
        {
            ice.SetActive(false);
            fire.SetActive(true);
        }
    }
}
