using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ComandosParaOJogador : MonoBehaviour
{
    private Vector3 rotacaoDoMouse;
    private Vector3 direcaoDoTeclado;
    private Vector3 direcaoDoControleDeToque;
    private bool sistemaAndroid;
    [SerializeField]
    private GameObject controlePorToque;
    [SerializeField]
    private GameObject botaoTrocaDeArmaAndroid;
    [SerializeField]
    private InputTecladoMouse controleTecladoMouse;
    [SerializeField]
    private MeuEventoDinamicoVector3 direcaoControleAnalogicoToque;
    [SerializeField]
    private MeuEventoDinamicoVector3 direcaoControlDoteclado;
    [SerializeField]
    private MeuEventoDinamicoVector3 rotacaoMouseControle;
    [SerializeField]
    private MeuEventoDinamicoVector3 rotacaoControleAnalogicoToque;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE //|| UNITY_EDITOR
        sistemaAndroid = false;
        controlePorToque.SetActive(false);
        botaoTrocaDeArmaAndroid.SetActive(false);
        Debug.Log("PC");
#endif
#if UNITY_ANDROID
        sistemaAndroid = true;
        controlePorToque.SetActive(true);
        botaoTrocaDeArmaAndroid.SetActive(true);
        Debug.Log("Android");
#endif
    }

    public void ObterDirecaoControleToque(Vector2 direcao)
    {
        direcaoDoControleDeToque = new Vector3(direcao.x, 0, direcao.y);       
    }

    public void ObterDirecaoTeclado(Vector3 direcao)
    {
        direcaoDoTeclado = direcao;
        //Debug.Log("SetDirecao"+direcaoDoTeclado);
    }

    public void ObterRotacaoMouse(Vector3 rotacao)
    {
        rotacaoDoMouse = rotacao;       
    }

    // Update is called once per frame
    void Update()
    {
        if (sistemaAndroid)
        {
            direcaoControleAnalogicoToque.Invoke(direcaoDoControleDeToque);
            rotacaoControleAnalogicoToque.Invoke(direcaoDoControleDeToque);
        }
        else
        {
            direcaoControlDoteclado.Invoke(direcaoDoTeclado);
            rotacaoMouseControle.Invoke(rotacaoDoMouse);
        }
    }
}
