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
    [SerializeField]
    private ObterStringEvent atualizarNome;
    [SerializeField]
    private string IDatual;

    public void Configurar(int posicao, string nome, string tempo, int pontuacao, string Id)
    {
        textoPosicao.text = posicao.ToString();
        textoNome.text = nome;
        textoTempo.text = tempo;
        textoPontuacao.text = pontuacao.ToString();
        IDatual = Id;
    }

    public void AtualizarNome(string novoNome)
    {
        textoNome.text = novoNome;
        atualizarNome.Invoke(novoNome);
    }

    public string getNome()
    {
        return textoNome.text;
    }

    public string getIDDessaLinha()
    {
        return IDatual;
    }
}
