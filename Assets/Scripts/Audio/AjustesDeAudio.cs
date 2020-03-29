using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AjustesDeAudio : MonoBehaviour
{
    //Privadas//
    [SerializeField]
    private AudioMixer MesaDeSom;
    [SerializeField]
    private string CanalDeAudio;
    [SerializeField]
    private Slider barra;
    //--------//

    private void Start()
    {
        CarregarPreDefinicoes();
    }

    private void CarregarPreDefinicoes()
    {
        if (PlayerPrefs.HasKey(CanalDeAudio))// verifica se algo foi salvo com o nome do parametroDeAudio atual
        {
            MesaDeSom.SetFloat(CanalDeAudio, PlayerPrefs.GetFloat(CanalDeAudio));
            barra.value = PlayerPrefs.GetFloat(CanalDeAudio);
        }
        else
        {
            MesaDeSom.SetFloat(CanalDeAudio, 0f);//Cria um valor inicial no player prefs caso não exista.
        }
    }

    public void AjustarSom(float intensidade)
    {
        MesaDeSom.SetFloat(CanalDeAudio, intensidade);
        PlayerPrefs.SetFloat(CanalDeAudio, intensidade);
    }

}
