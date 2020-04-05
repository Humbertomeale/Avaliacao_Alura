using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour, IReservavel
{
    //Privadas//
    [SerializeField]
    private float velocidade = 20;
    [SerializeField]
    private int danoDaBala = 1;
    private Rigidbody rigidbodyBala;
    private IReservaDeObjetos reserva;
    //-------//
    //Públicas//
    public AudioClip SomDeMorte;
    //-------//

    private void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbodyBala.MovePosition
            (rigidbodyBala.position +
            transform.forward * velocidade * Time.deltaTime);
    }

    public void SetReserva(IReservaDeObjetos reserva)
    {
        this.reserva = reserva;
    }

    public void MudarDanoDaBala(int dano)
    {
        danoDaBala = dano;
    }

    public void MudarVelocidadeDaBala(float v)
    {
        velocidade = v;
    }
    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward);
        switch(objetoDeColisao.tag)
        {
            case "Inimigo":
                ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo>();
                inimigo.TomarDano(danoDaBala);
                inimigo.ParticulaSangue(transform.position, rotacaoOpostaABala);
                break;
            case "ChefeDeFase":
                ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(danoDaBala);
                chefe.ParticulaSangue(transform.position, rotacaoOpostaABala);
            break;
        }

        this.reserva.DevolverObjeto(this.gameObject);
    }

    public void AoEntrarNaReserva()
    {
        this.gameObject.SetActive(false);
    }

    public void AoSairDaReserva()
    {
        this.gameObject.SetActive(true);
    }
}
