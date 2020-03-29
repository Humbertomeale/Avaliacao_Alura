using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoDinamico : MonoBehaviour
{
    private Text texto;

    void Awake()
    {
        this.texto = this.GetComponent<Text>();

    }

   public void AtualizarTexto(int numero)
    {
        texto.text = numero.ToString();
    }

    public void AtualizarTexto(string nome)
    {
        texto.text = nome;
    }

}