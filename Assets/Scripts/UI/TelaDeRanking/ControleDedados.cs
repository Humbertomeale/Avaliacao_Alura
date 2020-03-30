using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControleDedados : MonoBehaviour
{
    //Privadas//
    private Pontuador pontuaçãoFinal;
    private int pontos;
    private string tempo;
    private string nomeJogador;
    [SerializeField]
    private string nomeChavePlayerPref;
    //---------//
    private void Awake()
    {
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        pontuaçãoFinal = FindObjectOfType<Pontuador>();
        if (pontuaçãoFinal == null)
        {
            pontos = Random.Range(10,50);
            tempo = "1d:24h:59m:59s";
        }
        else
        {
            pontos = pontuaçãoFinal.Pontuacao();
            tempo = pontuaçãoFinal.TempoDeJogoFinalConvertido();
        }
        if (PlayerPrefs.HasKey(nomeChavePlayerPref))
        {
            nomeJogador = PlayerPrefs.GetString(nomeChavePlayerPref);
        }
        else
        {
            nomeJogador = "CallMeSusie";
            Debug.Log(nomeJogador);
        }
    }

    public string Nome()
    {
        return nomeJogador;
    }

    public int Pontos()
    {
        return pontos;
    }

    public string Tempo()
    {
        return tempo;
    }

    public void AlterarNomeJogador(string nome)
    {
        nomeJogador = nome;
        PlayerPrefs.SetString(nomeChavePlayerPref, nome);
    }
}
