using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuDePausa : MonoBehaviour
{
    //Privadas//
    private bool menuLigado = false;
    [SerializeField]
    private Sprite menuAtivo;
    [SerializeField]
    private Sprite menuInativo;
    [SerializeField]
    private GameObject painelDeMenu;
    private Image meuIcone;
    //--------//
    //Evestos Unity//
    [SerializeField]
    private UnityEvent aoClicar;

    //-------//

    private void Start()
    {
        meuIcone = GetComponent<Image>();

    }

    public void Clicando()
    {
        aoClicar.Invoke();
    }

    public void LigaDesliga()//Verifica se o menu esta ou não ativo.
    {
        if (menuLigado)
        {
            menuLigado = false;
            meuIcone.sprite = menuInativo;
            painelDeMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            menuLigado = true;
            meuIcone.sprite = menuAtivo;
            painelDeMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

}
