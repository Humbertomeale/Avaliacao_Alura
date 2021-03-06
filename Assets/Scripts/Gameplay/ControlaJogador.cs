﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
    //Variáveis//

    //Privadas//
    private Vector3 direcao;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    private Status statusJogador;
    [SerializeField]
    private AudioClip somDeDano;
    private ultimoEstadoJogador registroDeStatus;
    //---------//
    //Públicas//

    //-------//
    //Eventos Unity//
    [SerializeField]
    private ObterFloatEvent movimetarJogador;
    [SerializeField]
    private UnityEvent rotacionarJogador;
    [SerializeField]
    private UnityEvent aoMorrer;
    [SerializeField]
    private UnityEvent atualizandoBarraDeVida;
    [SerializeField]
    private ObterInteiro aoSofrerDano;
    [SerializeField]
    private ObterInteiro curandoJogador;
    //--------//

    //Métodos//

    private void Start()
    {
        if (aoSofrerDano == null) aoSofrerDano = new ObterInteiro();//verificação nula
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
        registroDeStatus = FindObjectOfType<ultimoEstadoJogador>();       
    }

    private void Update()
    {
        
        animacaoJogador.Movimentar(this.meuMovimentoJogador.Direcao.magnitude);
    }

    private void FixedUpdate()
    {
        movimetarJogador.Invoke(statusJogador.VelocidadeDeMovimento());
        rotacionarJogador.Invoke();
    }

    //Metodos Públicos//
    public int VidaMaxima()
    {
        return statusJogador.VidaInicial;
    }

    public void TomarDano (int dano)
    {
        aoSofrerDano.Invoke(dano);
        atualizandoBarraDeVida.Invoke();
        ControlaAudio.instancia.PlayOneShot(somDeDano);      
        if (statusJogador.VidaAtual() <= 0)
        {
            Morrer();
        }        
    }

    public void Morrer ()
    {
        aoMorrer.Invoke();
    }

    public void CurarVida (int quantidadeDeCura)
    {
        curandoJogador.Invoke(quantidadeDeCura);
        atualizandoBarraDeVida.Invoke();
    }

    public int VidaJogadorAtual()
    {
        return statusJogador.VidaAtual();
    }

    public void RegistrarUltimoEstado()
    {
        registroDeStatus.RegistrarVida(statusJogador.VidaAtual());

    }
}