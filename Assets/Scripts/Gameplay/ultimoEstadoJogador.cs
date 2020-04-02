using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ultimoEstadoJogador : MonoBehaviour
{
    private bool faseInicial = true;
    private int vidaJogador;

    public int ValorAnteriorDaVida()
    {
        return vidaJogador;
    }

    public void RegistrarVida(int vida)
    {
        vidaJogador = vida;
        faseInicial = false;
        /*Debug.Log("registrei");
        Debug.Log(vida);
        Debug.Log(faseInicial);*/
    }

    public bool PrimeiraFase()
    {
        return faseInicial;
    }
}
