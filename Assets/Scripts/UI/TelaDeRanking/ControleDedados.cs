using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControleDedados : MonoBehaviour
{
    //Privadas//
    private Pontuador pontuaçãoFinal;
    //---------//
    private void Awake()
    {
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        pontuaçãoFinal = FindObjectOfType<Pontuador>();
        Debug.Log(pontuaçãoFinal.Pontuacao());
    }

    public int Pontos()
    {
        var pts = pontuaçãoFinal.Pontuacao();
        return pts;
    }

    public string Tempo()
    {
        var t = pontuaçãoFinal.TempoDeJogoFinalConvertido();
        return t;
    }
}
