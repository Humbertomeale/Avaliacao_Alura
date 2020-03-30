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
    [SerializeField]
    private ObterStringEvent tempoDeJogoFormatado;

    public void AdicionarPontos()//Armazena a pontuação total.
    {
        pontos++;
        atualisarPontuacao.Invoke(pontos);
        //Debug.Log(pontos);
    }

    public void RegistrarTempoDeJogo()//armazena tempo total decorrido bruto.
    {
        tempoDeJogo = Time.timeSinceLevelLoad;
        tempoDeJogoFormatado.Invoke(TempoDeJogoFinalConvertido());
    }
    //---------//
    //Retorno de Dados//
    public int Pontuacao()
    {
        return pontos;

    }

    //Converte o tempo em segundos para o formato minutos/segundos.
    public string TempoDeJogoFinalConvertido()
    {
        var tempoTotalSegundos = Time.timeSinceLevelLoad;
        var minutos = (int)tempoTotalSegundos / 60;
        var segundos = (int)tempoTotalSegundos % 60;

        string tempo = string.Format("{0}min e {1}s", minutos, segundos);
        return tempo;
    }
}
