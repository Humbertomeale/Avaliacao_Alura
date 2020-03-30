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
        listaDeJogadores.AdicionarColocacao(dados.Nome(), dados.Tempo(), dados.Pontos());
        var quantidade = listaDeJogadores.Quantidade();
        var listaDeColocados = listaDeJogadores.PegarColocados();
        for (var i = 0; i < listaDeColocados.Count; i++)
        {
            if (i >= 5)
            {
                break;
            }
            preFabLinhas.GetComponent<LinhaDeDados>().Configurar(i + 1, listaDeColocados[i].Nome,listaDeColocados[i].Tempo, listaDeColocados[i].Pontos);
            Instantiate(preFabLinhas, painelRanking.transform);

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
    }

}
