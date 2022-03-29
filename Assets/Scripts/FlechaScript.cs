using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlechaScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int boss;
    public int limite;

    public Image Bossimg;

    public Sprite BossTuto;
    public Sprite Boss1;
    public Sprite Boss2;
    public Sprite Boss3;


    void Start()
    {
        Bossimg.sprite = BossTuto;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss < 1)
        {
            boss = 1;
        }
        if (boss > limite)
        {
            boss = limite;
        }

        ChangeImage();

    }

    void ChangeImage()
    {
        if (boss == 1)
        {
            Bossimg.sprite = BossTuto;
        }
        if (boss == 2)
        {
            Bossimg.sprite = Boss1;
        }
        if (boss == 3)
        {
            Bossimg.sprite = Boss2;
        }
        if (boss == 4)
        {
            Bossimg.sprite = Boss3;
        }
    }

    public void Izquierda()
    {
        boss -= 1;
    }

    public void Derecha()
    {
        boss += 1;
    }

}
