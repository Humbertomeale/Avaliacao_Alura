using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int VidaInicial = 100;
    private int Vida;
    [SerializeField]
    private float Velocidade = 5;

    private void Awake ()
    {
        Vida = VidaInicial;
    }

    public void ConfigurandoVidaAtual(int vida)
    {
        Vida = vida;       
    }

    public void CalculandoDano(int dn)
    {
        Vida -= dn;
    }

    public void CalculandoCura(int cura)
    {
        Vida += cura;
        if (Vida > VidaInicial)
        {
           Vida = VidaInicial;
        }
    }

    public int VidaAtual()
    {
        return this.Vida;
    }

    public float VelocidadeDeMovimento()
    {
        return this.Velocidade;
    }

    public void ResetVida()
    {
        Vida = VidaInicial;
    }


}
