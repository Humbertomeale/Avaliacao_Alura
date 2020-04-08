using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaStatus : MonoBehaviour, IArmavel
{
    //Privadas//
    [SerializeField]
    private string nomeDaArma;
    [SerializeField]
    private GameObject modelo3D;
    [SerializeField]
    private int dano = 1;
    [SerializeField]
    private float cadenciaDetiro = 1f;
    [SerializeField]
    private float velocidadeDaBala = 20f;
    [SerializeField]
    private float precisaoDetiro = 1f;
    [SerializeField]
    private bool disparoAutomatico = false;
    [SerializeField]
    private AudioClip somDoTiro;
    //-------//
    public string PegarNome()
    {
        return nomeDaArma;
    }

    public int PegarDano()
    {
        return dano;
    }

    public float PegarCadencia()
    {
        return cadenciaDetiro;
    }

    public float PegarVelocidadeDaBala()
    {
        return velocidadeDaBala;
    }

    public float PegarPrecisao()
    {
        return precisaoDetiro;
    }

    public bool PegarTipoDeDisparo()
    {
        return disparoAutomatico;
    }

    public void AtivarModelo3D()
    {
        modelo3D.SetActive(true);
    }

    public void DesativarModelo3D()
    {
        modelo3D.SetActive(false);
    }
    public AudioClip PegarSomDoTiro()
    {
        return somDoTiro;
    }
}
