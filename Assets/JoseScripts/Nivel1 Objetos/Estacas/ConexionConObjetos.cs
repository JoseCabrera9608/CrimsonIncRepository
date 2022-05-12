using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConexionConObjetos : MonoBehaviour
{
    public static ConexionConObjetos current;


    private void Awake()
    {
        current = this;
    }

    public event Action<int> Conexion;

     public void Conectar(int id)
     {

        if (Conexion != null)
        {
            Conexion(id);
        }
    }
    
}
