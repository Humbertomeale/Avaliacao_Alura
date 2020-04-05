using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    [SerializeField]
    private CaixaDeSom caixa;
    private Vector3 rotacaoJogador;

    public void AudioPasso()
    {
        caixa.Tocar();
    }

    public void RotacionarJogadorMouse(Vector3 rotacao)
    {
        if (rotacao != Vector3.zero)
        {
            Vector3 posicaoMiraJogador = rotacao;
            posicaoMiraJogador.y = transform.position.y;
            rotacaoJogador = posicaoMiraJogador;
        }
    }

    public void RotacaoJogadorBotaoAnalogicoToque(Vector3 rotacao)
    {
        if (rotacao != Vector3.zero)
        {
            Vector3 posicaoMiraJogador = rotacao;
            posicaoMiraJogador.y = transform.position.y;
            rotacaoJogador = posicaoMiraJogador;
        }
    }

    public void RotacaoJogador()
    {
        //if(Direcao != Vector3.zero)
        //{
           //Vector3 posicaoMiraJogador = this.Direcao;
            //posicaoMiraJogador.y = transform.position.y;
            Rotacionar(rotacaoJogador);
        //}
    }

}
