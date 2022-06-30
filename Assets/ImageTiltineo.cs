using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTiltineo : MonoBehaviour
{

    public Image image;
    public float timer;
    public float timer2;
   

    // Start is called before the first frame update
    void Start()
    {
        image.color = new Color32(255, 255, 225, 255);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 2)
        {
            timer2 += 2 * Time.deltaTime;

        }
        if (timer >= 2 && timer <= 4)
        {
            timer2 -= 2 * Time.deltaTime;


        }
        if (timer > 4)
        {
            timer = 0;
        }
    }
}
