using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MostrarRegistro : MonoBehaviour
{
    //Privadas//
    [SerializeField]
    private ControleDedados dados;
    //---------//
    //Unity events//
    [SerializeField]
    private ObterInteiro mostrarPontos;
    [SerializeField]
    private ObterStringEvent mostrarTempo;
    //---------//


    void Awake()
    {
        if (dados == null)
        {
            dados = GetComponent<ControleDedados>();
        }
    }

    private void Start()
    {
        AtualizarPontos();
        AtualizarTempo();
    }

    public void AtualizarPontos()
    {
        mostrarPontos.Invoke(dados.Pontos());
    }
    public void AtualizarTempo()
    {
        mostrarTempo.Invoke(dados.Tempo());
    }

}
