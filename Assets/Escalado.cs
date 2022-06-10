using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalado : MonoBehaviour
{
    public Vector3 finalScale;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        //timer += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.localScale = new Vector3(0.5f * timer, transform.localScale.y, 0.5f * timer);
    }
}
