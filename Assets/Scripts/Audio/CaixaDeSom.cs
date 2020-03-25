using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CaixaDeSom : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] listaDeAudio;
    private AudioSource saidaDeAudio;

    private void Awake()
    {
        saidaDeAudio = GetComponent<AudioSource>();
    }

    public void Tocar()
    {
        var sorteado = Random.Range(0, listaDeAudio.Length); // Serteia um inteiro no limiter do conteudo da lista de audio.
        saidaDeAudio.PlayOneShot(listaDeAudio[sorteado]);
    }

}
