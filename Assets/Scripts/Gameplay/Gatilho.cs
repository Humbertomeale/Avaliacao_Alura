using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gatilho : MonoBehaviour
{
    [SerializeField]
    private UnityEvent acionarMetodo;
    private GameObject objetoQueMeAcionou;
    [SerializeField]
    private string tagObjeto;

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        objetoQueMeAcionou = objetoDeColisao.gameObject;
        if(objetoQueMeAcionou.tag == tagObjeto)
        {
            acionarMetodo.Invoke();
        }
    }

    public GameObject quemMeAcionou()
    {
        return objetoQueMeAcionou;
    }
}
