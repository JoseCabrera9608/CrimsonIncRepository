using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModifier : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform Obstruction;
    public Transform Target, Player;
    float zoomSpeed = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        ViewObstructed();
    }

    void ViewObstructed()
    {

        RaycastHit hit;


        if (Obstruction)
        {
            Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 7f))
        {
            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Enemy") //Aqui ponemos todo los tags de lo que no queremos que desaparezca owo, by Santi
            {
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if (Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }


            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if (Vector3.Distance(transform.position, Target.position) < 7.5f)
                {
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }



            }
        }


    }
}
