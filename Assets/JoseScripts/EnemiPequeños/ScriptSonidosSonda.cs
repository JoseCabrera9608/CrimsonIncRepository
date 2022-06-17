using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSonidosSonda : MonoBehaviour
{
    public AudioSource muerte;
    public AudioSource disparo;
    public AudioSource golpeMelee;
    public AudioSource magnetismo;
    public AudioSource da�oPorPlayer;
    public AudioSource kamikazeContador;

   public void SonidoMuerte()
    {
        muerte.Play();
    }

    public void SonidoDisparo()
    {
        disparo.Play();
    }

    public void SonidoGolpe()
    {
        golpeMelee.Play();
    }
    public void SonidoMagnetismo()
    {
        magnetismo.Play();
    }

    public void Da�oPorPlayer()
    {
        da�oPorPlayer.Play();
    }
    public void ContadorKamikaze()
    {
        kamikazeContador.Play();
    }
}
