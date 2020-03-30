using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinhaDeDados : MonoBehaviour
{
    [SerializeField]
    private Text textoPosicao;
    [SerializeField]
    private Text textoNome;
    [SerializeField]
    private Text textoTempo;
    [SerializeField]
    private Text textoPontuacao;

    public void Configurar(int posicao, string nome, string tempo, int pontuacao)
    {
        textoPosicao.text = posicao.ToString();
        textoNome.text = nome;
        textoTempo.text = tempo;
        textoPontuacao.text = pontuacao.ToString();


    }
}
