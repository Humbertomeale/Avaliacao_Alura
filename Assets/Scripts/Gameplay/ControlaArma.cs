using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlaArma : MonoBehaviour
{
    //Privadas//
    private float velocidadaDaBala = 20f;
    private float cadenciaDeTiro = 5;
    private float tempoEntreDisparos = 1f;
    private int danoDaBala = 1;
    private string nomeDaArama;
    private bool automatica = false;
    private bool tiroContinuo = true;
    private bool prontoParaatirar = true;
    [SerializeField]
    private GameObject armaSlot_01;
    private bool slot01Ativo;
    [SerializeField]
    private GameObject armaSlot_02;
    private bool slot02Ativo;
    [SerializeField]
    private ObterStringEvent nomeDaArmaUI;
    [SerializeField]
    private int quantidadeDeTirosContinuosAndroid = 5;
    //--------//
    //Publicas//
    public ReservaExtensivel reservaDeBalas;
    public GameObject CanoDaArma;
    public AudioClip SomDoTiro;
    //-------//

    private void Start()
    {
        ConfigurarArma(armaSlot_01);
        slot01Ativo = true;
#if UNITY_ANDROID
        nomeDaArmaUI.Invoke(nomeDaArama);
#endif
    }

    public void AlternarSlots()
    {
        if (slot01Ativo)
        {
            slot01Ativo = false;
            armaSlot_01.GetComponent<ArmaStatus>().DesativarModelo3D();
            ConfigurarArma(armaSlot_02);
            slot02Ativo = true;
        }
        else
        {
            slot02Ativo = false;
            armaSlot_02.GetComponent<ArmaStatus>().DesativarModelo3D();
            ConfigurarArma(armaSlot_01);
            slot01Ativo = true;
        }
    }
    private void Update()
    {
        StartCoroutine(trocarArma());

        if (automatica)
        {
            #if UNITY_STANDALONE
            StartCoroutine(tiroPorBotaoDoMouseContinuo());
#endif
            //////
#if UNITY_ANDROID
            StartCoroutine(tiroPorToqueAndroidContinuo());
#endif
        }
        else
        {
#if UNITY_ANDROID
            StartCoroutine(tiroPorToqueAndroid());
#endif
#if UNITY_STANDALONE
            StartCoroutine(tiroPorBotaoDoMouse());
#endif
        }


    }

    public void ConfigurarArma(GameObject arma)
    {
        var minhArma = arma.GetComponent<ArmaStatus>();
        minhArma.AtivarModelo3D();
        velocidadaDaBala = minhArma.PegarVelocidadeDaBala();
        cadenciaDeTiro = minhArma.PegarCadencia();
        danoDaBala = minhArma.PegarDano();
        nomeDaArama = minhArma.PegarNome();
        automatica = minhArma.PegarTipoDeDisparo();
        SomDoTiro = minhArma.PegarSomDoTiro();
#if UNITY_ANDROID
        nomeDaArmaUI.Invoke(nomeDaArama);
#endif
    }

    private IEnumerator trocarArma()
    {
        if (Input.GetKeyDown("q"))
        {
            AlternarSlots();
            Debug.Log("Troquei");
        }
        yield return null;
    }

    private IEnumerator tiroPorToqueAndroid()
    {
        var toqueNaTela = Input.touches;
        foreach (var toque in toqueNaTela)
        {
            if (toque.phase == TouchPhase.Began)
            {
                // this.Atirar();
                // ControlaAudio.instancia.PlayOneShot(SomDoTiro);
                if (prontoParaatirar)
                {
                    prontoParaatirar = false;
                    this.Atirar();
                    ControlaAudio.instancia.PlayOneShot(SomDoTiro);
                    yield return new WaitForSeconds(intervaloentreDisparos());
                    prontoParaatirar = true;
                }

            }
        }
        yield return null;
    }
    private IEnumerator tiroPorToqueAndroidContinuo()
    {
        var toqueNaTela = Input.touches;
        foreach (var toque in toqueNaTela)
        {
            if (toque.phase == TouchPhase.Began && tiroContinuo)
            {
                if (prontoParaatirar)
                {
                    prontoParaatirar = false;
                    tiroContinuo = false;
                    var i = 1;
                    while (i < quantidadeDeTirosContinuosAndroid)
                    {
                        this.Atirar();
                        ControlaAudio.instancia.PlayOneShot(SomDoTiro);
                        i++;
                        yield return new WaitForSeconds(intervaloentreDisparos());
                    }
                    prontoParaatirar = true;
                    tiroContinuo = true;
                }

            }
        }
        yield return null;
    }

    private IEnumerator tiroPorBotaoDoMouse()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (prontoParaatirar)
            {
                prontoParaatirar = false;
                this.Atirar();
                ControlaAudio.instancia.PlayOneShot(SomDoTiro);
                yield return new WaitForSeconds(intervaloentreDisparos());
                prontoParaatirar = true;
            }
        }
        yield return null;
    }

    private IEnumerator tiroPorBotaoDoMouseContinuo()
    {
        if (Input.GetButton("Fire1") && tiroContinuo)
        {
            tiroContinuo = false;
            this.Atirar();
            ControlaAudio.instancia.PlayOneShot(SomDoTiro);
            yield return new WaitForSeconds(intervaloentreDisparos());
            tiroContinuo = true;          
        }
        yield return null;
    }

    private float intervaloentreDisparos()
    {
        var intervalo = tempoEntreDisparos / cadenciaDeTiro;
        return intervalo;
    }

    private void Atirar()
    {
        if (this.reservaDeBalas.TemObjeto())
        {
            var bala = this.reservaDeBalas.PegarObjeto();
            bala.GetComponent<Bala>().MudarVelocidadeDaBala(velocidadaDaBala);
            bala.GetComponent<Bala>().MudarDanoDaBala(danoDaBala);
            bala.transform.position = CanoDaArma.transform.position;
            bala.transform.rotation = CanoDaArma.transform.rotation;
        }
    }
}
