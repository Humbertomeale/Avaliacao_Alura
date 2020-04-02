using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour{
    //privadas//
    [SerializeField]
    private string proximoNivel;
    private ControlaJogador scriptControlaJogador;
    [SerializeField]
    private string cenaDeGameover;
    private float tempoPontuacaoSalvo;
    [SerializeField]
    private Slider SliderVidaJogador;
    [SerializeField]
    private GameObject PainelDeGameOver;
    [SerializeField]
    private GameObject painelDeFimDeFase;
    [SerializeField]
    private Text TextoChefeAparece;
    private Pontuador meuPontuador;
    //---------//
    //Unity Events//   
    [SerializeField]
    private ObterInteiro pontuacaoAtual;
    [SerializeField]
    private ObterStringEvent tempoFinalFormatado;
    //--------//

	// Use this for initialization
    private void Awake()
    {

    }

    void Start () {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptControlaJogador.VidaJogadorAtual();
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;       
       //meuPontuador = FindObjectOfType<Pontuador>();
        meuPontuador= FindObjectOfType(typeof(Pontuador)) as Pontuador;
        meuPontuador.AdicionarInterface(this);
        AtualizarPontuacao();
        if (meuPontuador.GetComponent<ultimoEstadoJogador>().PrimeiraFase() == false)
        {
            scriptControlaJogador.GetComponent<Status>().ConfigurandoVidaAtual(
            meuPontuador.GetComponent<ultimoEstadoJogador>().ValorAnteriorDaVida());
            AtualizarSliderVidaJogador();
        }
        else
        {
            //AtualizarSliderVidaJogador();
        }
    }

    public void AtualizarSliderVidaJogador ()
    {
        SliderVidaJogador.value = scriptControlaJogador.VidaJogadorAtual();
    }

    public void AtualizarPontuacao()
    {
        pontuacaoAtual.Invoke(meuPontuador.Pontuacao());
    }

    public void GameOver ()
    {
        PainelDeGameOver.SetActive(true);
        meuPontuador.RegistrarTempoDeJogo();
        tempoFinalFormatado.Invoke(meuPontuador.TempoDeJogoFinalConvertido());

        Time.timeScale = 0;
    }

    public void PasseiDeFase()
    {
        painelDeFimDeFase.SetActive(true);
        scriptControlaJogador.RegistrarUltimoEstado();

        Time.timeScale = 0;
    }

    public void Reiniciar ()
    {
        SceneManager.LoadScene(cenaDeGameover);
    }

    public void ProximoNivel()
    {
        SceneManager.LoadScene(proximoNivel);
    }

    public void AparecerTextoChefeCriado ()
    {
        StartCoroutine(DesaparecerTexto(2, TextoChefeAparece));
    }

    IEnumerator DesaparecerTexto (float tempoDeSumico, Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while (textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;
            if(textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
