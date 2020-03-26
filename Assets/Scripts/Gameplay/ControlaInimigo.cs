using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel, IReservavel
{
    //Variáveis//

    //Privadas//
    private GameObject Jogador;
    [SerializeField]
    private float campoDeVisão = 15f;
    [SerializeField]
    private float alcanceDoAtaque = 2.5f;
    [SerializeField]
    private float raioDeposicaoAleatoria = 10;
    [SerializeField]
    private int danoMin = 20;
    [SerializeField]
    private int danoMax = 30;
    private float ajusteProximidadeDestino = 0.05f;   
    private ControlaInterface scriptControlaInterface;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    [SerializeField]
    private AudioClip SomDeMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao = Vector3.zero;
    private float contadorVagar;
    private IReservaDeObjetos reserva;
    private float tempoEntrePosicoesAleatorias = 4;
    private float porcentagemGerarKitMedico = 0.1f;
    [SerializeField]
    private GameObject KitMedicoPrefab;
    //---------//
    //Unity Events//
    [SerializeField]
    private ObterInteiro aoSofrerDano;
    //-----------//
    //Públicas//
    [HideInInspector]
    public GeradorZumbis meuGerador;
    public GameObject ParticulaSangueZumbi;
    //------//

    //Métodos//

    public void SetReserva(IReservaDeObjetos reserva)
    {
        this.reserva = reserva;
    }
    private void Awake()
    {
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
    }

    void Start () {
        Jogador = GameObject.FindWithTag("Jogador");
        AleatorizarZumbi();
        statusInimigo = GetComponent<Status>();
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if(distancia > campoDeVisão)
        {
            Vagar ();
        }
        else if (distancia > alcanceDoAtaque)
        {
            direcao = Jogador.transform.position - transform.position;
            movimentaInimigo.SetDirecao(direcao);
            movimentaInimigo.Movimentar(statusInimigo.VelocidadeDeMovimento());

            animacaoInimigo.Atacar(false);
        }
        else
        {
            direcao = Jogador.transform.position - transform.position;

            animacaoInimigo.Atacar(true);
        }
    }

    void Vagar ()
    {
        contadorVagar -= Time.deltaTime;
        if(contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= ajusteProximidadeDestino;
        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.SetDirecao(direcao);
            movimentaInimigo.Movimentar( statusInimigo.VelocidadeDeMovimento());
        }           
    }

    Vector3 AleatorizarPosicao ()
    {
        Vector3 posicao = Random.insideUnitSphere * raioDeposicaoAleatoria;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    private void AtacaJogador ()
    {
        int dano = Random.Range(danoMin, danoMax);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbi ()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        aoSofrerDano.Invoke(dano);
       // statusInimigo.Vida() -= dano;
        if(statusInimigo.VidaAtual() <= 0)
        {
            Morrer();
        }
    }

    public void ParticulaSangue (Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        Invoke("VoltarParaReserva", 2);
        animacaoInimigo.Morrer();
        movimentaInimigo.Morrer();
        this.enabled = false;
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        
    }

    private void VoltarParaReserva()
    {       
        this.reserva.DevolverObjeto(this.gameObject);
    }

    private void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }

    public void AoEntrarNaReserva()
    {
        this.movimentaInimigo.Reiniciar();
        this.enabled = true;
        this.gameObject.SetActive(false);
    }

    public void AoSairDaReserva()
    {
        this.gameObject.SetActive(true);
    }
}
