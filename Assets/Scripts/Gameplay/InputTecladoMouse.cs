using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputTecladoMouse : MonoBehaviour
{
    [SerializeField]
    private GameObject jogador;
    [SerializeField]
    private LayerMask mascaraChao;
    private Vector3 rotacao;
    private Vector3 direcao;
    [SerializeField]
    private MeuEventoDinamicoVector3 mouseDirecao;
    [SerializeField]
    private MeuEventoDinamicoVector3 movimentoDirecao;
   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        direcao = new Vector3(eixoX, 0, eixoZ);
        RotacaoJogador(mascaraChao);
        mouseDirecao.Invoke(rotacao);
        movimentoDirecao.Invoke(direcao);
        //Debug.Log(rotacao);
    }


    public Vector3 DirecaoDeMovimento()
    {
        return direcao;
    }

    public void RotacaoJogador(LayerMask MascaraChao)
    {
        ///rotação
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        ///desenhando o raio para debug, o seja, para vermos
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;
        if (Physics.Raycast(raio, out impacto, 100, MascaraChao)) /* adicionamos mascara chão para limitar a colisão
            apenas com essa mascara evitando travamentos na rotação*/
        {
            /// gera vetor de impacto para mira do jogador
            Vector3 posicaoMiraJogador = impacto.point - jogador.transform.position;
            /// travamos y do vetor igualando com o y do jogador
            posicaoMiraJogador.y = jogador.transform.position.y;
            rotacao = posicaoMiraJogador;           
        }
    }

    public Vector3 ObterRotacao()
    {
        return rotacao;
    }


}

[Serializable]
public class MeuEventoDinamicoVector3 : UnityEvent<Vector3> { }
