using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField]
    private float porcentagemGerarKitMedico = 0.1f;
    [SerializeField]
    private GameObject KitMedicoPrefab;
    [SerializeField]
    private GameObject ParticulaSangueZumbi;
    //---------//
    //Públicas//
    [HideInInspector]
    public GeradorZumbis meuGerador;
    //------// 
    //Unity Events//
    [SerializeField]
    private ObterInteiro aoSofrerDano;
    [SerializeField]
    private UnityEvent aoMorrer;
    //-----------//
     
   
    private void Awake()
    {
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
    }

    void Start () {
        if (aoSofrerDano == null) aoSofrerDano = new ObterInteiro();//verificação nula
        Jogador = GameObject.FindWithTag("Jogador");
        AleatorizarZumbi();
        statusInimigo = GetComponent<Status>();
        scriptControlaInterface = FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }
    //--------//

    //Updates e movimentação//
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
    //--------//

    //Metodos públicos e calculos de parâmetros//
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
        aoMorrer.Invoke();
        this.enabled = false;

        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        
    }
    public void TocarSomMorte()
    {
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
    }

    public void VerificarGeracaoKitMedico()
    {
        if (Random.value <= this.porcentagemGerarKitMedico)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
    //----------//

    //Metodos de Reservas//
    public void SetReserva(IReservaDeObjetos reserva)
    {
        this.reserva = reserva;
    }

    private void VoltarParaReserva()
    {       
        this.reserva.DevolverObjeto(this.gameObject);
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
