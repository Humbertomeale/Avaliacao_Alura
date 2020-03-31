using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MostrarRegistro : MonoBehaviour
{
    //Privadas//
    [SerializeField]
    private ControleDedados dados;
    [SerializeField]
    private GameObject preFabLinhas;
    [SerializeField]
    private GameObject painelRanking;
    [SerializeField]
    private SalvadorDePlacar listaDeJogadores;
    private string iD;
    [SerializeField]
    private int numeroVisivelDeLinhas = 5;
    private string antigoNome;

    //---------//
    //Unity events//
    [SerializeField]
    private ObterInteiro mostrarPontos;
    [SerializeField]
    private ObterStringEvent mostrarTempo;
    [SerializeField]
    private ObterStringEvent atualizaNome;

    //---------//


    void Awake()
    {
        if (dados == null)
        {
            dados = GetComponent<ControleDedados>();
        }
        if(listaDeJogadores == null)
        {
            listaDeJogadores = GetComponent<SalvadorDePlacar>();
        }

    }

    private void Start()
    {
        AtualizarPontos();
        AtualizarTempo();
        AtualizarNome();
        iD = listaDeJogadores.AdicionarColocacao(dados.Nome(), dados.Tempo(), dados.Pontos());
        antigoNome = dados.Nome();
        gerarLista();
        /*var quantidade = listaDeJogadores.Quantidade();
        var listaDeColocados = listaDeJogadores.PegarColocados();
        for (var i = 0; i < listaDeColocados.Count; i++)
        {
            if (i >= 5)
            {
                break;
            }
            preFabLinhas.GetComponent<LinhaDeDados>().Configurar(i + 1, listaDeColocados[i].Nome,listaDeColocados[i].Tempo, listaDeColocados[i].Pontos);
            Instantiate(preFabLinhas, painelRanking.transform);
        }*/
    }

    private void gerarLista()
    {
        var quantidade = listaDeJogadores.Quantidade();
        var listaDeColocados = listaDeJogadores.PegarColocados();
        for (var i = 0; i < listaDeColocados.Count; i++)
        {
            if (i >= numeroVisivelDeLinhas)
            {
                break;
            }
            preFabLinhas.GetComponent<LinhaDeDados>().Configurar(i + 1, listaDeColocados[i].Nome, listaDeColocados[i].Tempo, listaDeColocados[i].Pontos);
            Instantiate(preFabLinhas, painelRanking.transform);


        }
    }

    private void mudarValorNaLista(string novoNome)
    {
        int children = painelRanking.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            var linha = painelRanking.GetComponentInChildren<LinhaDeDados>();
            var n = linha.getNome();
            if(n == antigoNome)
            {
                linha.AtualizarNome(dados.Nome());
                antigoNome = dados.Nome();
                break;
            }
        }
    }

    public void AtualizarPontos()
    {
        mostrarPontos.Invoke(dados.Pontos());
    }
    public void AtualizarTempo()
    {
        mostrarTempo.Invoke(dados.Tempo());
    }
    public void AtualizarNome()
    {
        atualizaNome.Invoke(dados.Nome());
        listaDeJogadores.AlterarNome(dados.Nome(),iD);
        mudarValorNaLista(dados.Nome());
    }

}
