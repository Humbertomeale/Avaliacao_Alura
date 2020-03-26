using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int VidaInicial = 100;
    [HideInInspector]
    public int Vida;
    public float Velocidade = 5;

    private void Awake ()
    {
        Vida = VidaInicial;
    }

    public void calculandoDano(int dn)
    {
        Vida -= dn;
        Debug.Log(dn);
    }

    public int VidaAtual()
    {
        return this.Vida;
    }
}
