using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosBoss0 : MonoBehaviour
{
    public void SonidoMagneto()
    {
        FindObjectOfType<AudioManager>().Play("MagnetoBoss");
    }

    public void SonidoGolpes()
    {
        FindObjectOfType<AudioManager>().Play("GolpesBoss");
    }

    public void SonidoPasos()
    {
        FindObjectOfType<AudioManager>().Play("PasosBoss");
    }

    public void SonidosSlash()
    {
        FindObjectOfType<AudioManager>().Play("SonidosSlashs");
    }
    public void SonidoMuerte()
    {
        FindObjectOfType<AudioManager>().Play("MuerteBoss");
    }
}
