using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    [SerializeField]
    private CaixaDeSom caixa;

    public void AudioPasso()
    {
        caixa.Tocar();
    }

    public void RotacaoJogador()
    {
        if(Direcao != Vector3.zero)
        {
            Vector3 posicaoMiraJogador = this.Direcao;
            posicaoMiraJogador.y = transform.position.y;
            Rotacionar(posicaoMiraJogador);
        }

    }
}
