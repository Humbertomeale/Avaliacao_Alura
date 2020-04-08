using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private string antigoNomeID;

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
        gerarLista();
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
            preFabLinhas.GetComponent<LinhaDeDados>().Configurar(i + 1, listaDeColocados[i].Nome, listaDeColocados[i].Tempo, listaDeColocados[i].Pontos, listaDeColocados[i].ID);
            Instantiate(preFabLinhas, painelRanking.transform);
        }
    }

    private void mudarValorNaLista(string novoNome, string iDAntigo)
    {
        int children = painelRanking.transform.childCount;
        LinhaDeDados[] linha;
        linha = painelRanking.GetComponentsInChildren<LinhaDeDados>();
        for (int i = 1; i < children; i++)
        {
            var id = linha[i].getIDDessaLinha();           
            if(id == iDAntigo)
            {
                linha[i].AtualizarNome(dados.Nome());
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
        listaDeJogadores.AlterarNome(dados.Nome(),iD);
        //mudarValorNaLista(dados.Nome(), iD);
        atualizaNome.Invoke(dados.Nome());
    }

    public void MudardadoNaTela()
    {
        mudarValorNaLista(dados.Nome(), iD);
    }

}
