using System;
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
    //---------//
    //Públicas//
    public AudioClip SomDeDano;
    public Status statusJogador;
    //-------//
    //Eventos Unity//
    [SerializeField]
    private UnityEvent aoMorrer;
    [SerializeField]
    private UnityEvent atualizandoBarraDeVida;
    //--------//

    //Métodos//

    private void Start()
    {
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    void Update()
    {
        
        animacaoJogador.Movimentar(this.meuMovimentoJogador.Direcao.magnitude);
    }

    void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(statusJogador.Velocidade);
        meuMovimentoJogador.RotacaoJogador();
    }

    public void TomarDano (int dano)
    {
        statusJogador.Vida -= dano;
        atualizandoBarraDeVida.Invoke();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        if(statusJogador.Vida <= 0)
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
        statusJogador.Vida += quantidadeDeCura;
        if(statusJogador.Vida > statusJogador.VidaInicial)
        {
            statusJogador.Vida = statusJogador.VidaInicial;
        }
        atualizandoBarraDeVida.Invoke();
    }
}
