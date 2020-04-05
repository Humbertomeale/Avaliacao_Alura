using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    //Privadas//
    private float velocidadaDaBala = 20f;
    private int cadenciaDeTiro = 1;
    private float tempoEntreDisparos = 1f;
    private float danoDaBala;
    private bool prontoParaatirar = true;
    [SerializeField]
    private GameObject armaSlot_01;
    [SerializeField]
    private GameObject armaSlot_02;
    //--------//
    //Publicas//
    public ReservaExtensivel reservaDeBalas;
    public GameObject CanoDaArma;
    public AudioClip SomDoTiro;
    //-------//

    private void Update()
    {
        //StartCoroutine(tiroPorToqueAndroid());
        StartCoroutine(tiroPorBotaoDoMouse());

    }

    private IEnumerator tiroPorToqueAndroid()
    {
        var toqueNaTela = Input.touches;
        foreach (var toque in toqueNaTela)
        {
            if (toque.phase == TouchPhase.Began)
            {
                this.Atirar();
                ControlaAudio.instancia.PlayOneShot(SomDoTiro);
            }
        }
        yield return null;
    }

    private IEnumerator tiroPorBotaoDoMouse()
    {
        while (Input.GetButtonDown("Fire1"))
        {
            if (prontoParaatirar)
            {
                prontoParaatirar = false;
                Debug.Log(prontoParaatirar);
                this.Atirar();
                ControlaAudio.instancia.PlayOneShot(SomDoTiro);
                Debug.Log("Atirei" + prontoParaatirar);
                yield return new WaitForSeconds(tempoEntreDisparos);
                prontoParaatirar = true;
                Debug.Log(prontoParaatirar);
            }

        }
        yield return null;

    }
   
   

    private void Atirar()
    {
        if (this.reservaDeBalas.TemObjeto())
        {
            var bala = this.reservaDeBalas.PegarObjeto();
            bala.transform.position = CanoDaArma.transform.position;
            bala.transform.rotation = CanoDaArma.transform.rotation;
        }
    }
}
