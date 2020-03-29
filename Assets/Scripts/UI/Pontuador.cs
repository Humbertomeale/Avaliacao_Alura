using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pontuador : MonoBehaviour
{
    private int pontos = 0;
    private float tempoDeJogo = 0f;
    private string nomeDoJogador;

    [SerializeField]
    private ObterInteiro atualisarPontuacao;

    public void AdicionarPontos()
    {
        pontos++;
        atualisarPontuacao.Invoke(pontos);
        //Debug.Log(pontos);
    }

    public void RegistrarTempoDeJogo(float tempo)
    {
        tempoDeJogo = tempo;
    }
    //---------//
    //Retorno de Dados//
    public int Pontuacao()
    {
        return pontos;

    }

    public float TempoDeJogoFinal()
    {
        return tempoDeJogo;
    }
}
