using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool pausado = false;
    public PlayerInputActions controle_player;
    private InputAction movimento, runSlide, pular, atirar, empurrar, correr;
    public float velocidade = 150;
    private float velocidade_deslocamento = 0;
    private float rotacao_deslocamento = 0;
    public float velocidade_empurrar = 150;
    public float velocidade_andar = 100;
    public float velocidade_correr = 150;
    public float velocidade_pulo = 150;
    public float rotacao_walk = 5;
    public float rotacao_run = 8;
    public float pulo = 6;
    private Animator corpo_fsm;
    private Rigidbody corpo_fisico;
    public bool empurrando = false;
    private bool correndo = false;
    public bool atirando = false;
    public bool area_empurrar = false;
    private bool slider = false;
    public LayerMask piso, objetos_gerais;
    public GameObject Projetil;
    void Awake()
    {
        controle_player = new PlayerInputActions();
    }

    void OnEnable()
    {
        movimento = controle_player.Player.Move; //seta junto com o input sistem a animação
        movimento.Enable();

        runSlide = controle_player.Player.Slide;
        runSlide.Enable();
        runSlide.performed += RunningSlide;

        atirar = controle_player.Player.Fire;
        atirar.Enable();
        atirar.performed += Atirar;

        pular = controle_player.Player.Jump;
        pular.Enable();
        pular.performed += Pular;

        empurrar = controle_player.Player.Push;
        empurrar.Enable();
        empurrar.performed += Empurrada;

        correr = controle_player.Player.Run;
        correr.Enable();
        correr.performed += Correr;
    }

    void OnDisable()
    {
        movimento.Disable();
        runSlide.Disable();
        atirar.Disable();
        pular.Disable();
        empurrar.Disable();
        correr.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        corpo_fisico = transform.GetComponent<Rigidbody>();
        corpo_fsm = transform.GetComponent<Animator>();

        velocidade_deslocamento = velocidade_andar;
        rotacao_deslocamento = rotacao_walk;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (pausado == false)
        {
            Movimentar();
        }
    }

    void Movimentar()
    {
        //Movimentos básicos

        if (empurrando == true && area_empurrar == true)
        {
            velocidade_deslocamento = velocidade_empurrar;
            corpo_fsm.SetFloat("mover", .5f);
        }
        else if (slider == true)
        {
            velocidade_deslocamento = velocidade_correr;
            corpo_fsm.SetFloat("mover", 1f);
        }
        else
        {
            velocidade_deslocamento = velocidade_andar;
            corpo_fsm.SetFloat("mover", 0f);
        }

        bool emSolo = Physics.CheckSphere(transform.GetChild(0).transform.position, 0.5f, piso, QueryTriggerInteraction.Ignore);
        
        if (emSolo == false)
        {
            velocidade_deslocamento = velocidade_pulo;
        }

        Vector3 velocidade_fisica = Vector3.zero;

        //Frente e Voltar
        if (movimento.ReadValue<Vector2>().y > 0)
        {
            velocidade_fisica = transform.forward * movimento.ReadValue<Vector2>().y * velocidade_deslocamento * Time.fixedDeltaTime;
        }
        else
        {
            //Rotacionar
            transform.Rotate(0, movimento.ReadValue<Vector2>().y * rotacao_deslocamento, 0);
        }

        //Queda
        velocidade_fisica.y = (corpo_fisico.velocity.y < 0) ? corpo_fisico.velocity.y * 1.03f : corpo_fisico.velocity.y;

        corpo_fisico.velocity = velocidade_fisica;

        //Rotacionar
        transform.Rotate(0, movimento.ReadValue<Vector2>().x * rotacao_deslocamento, 0);

        //Alterando a Maquina de Estados

        if (movimento.ReadValue<Vector2>().y != 0)
        {
            corpo_fsm.SetBool("movimentando", true);
        }
        else
        {
            corpo_fsm.SetBool("movimentando", false);
        }

        //Restaurar Velocidade das Animações
        bool poderRestaurar = Physics.CheckSphere(transform.GetChild(1).transform.position, 0.5f, piso, QueryTriggerInteraction.Ignore);

        if (poderRestaurar)
        {
            corpo_fsm.speed = 1;
        }
    }

    public void Empurrada(InputAction.CallbackContext context)
    {
        Debug.Log("Espadada");
        empurrando = !empurrando;
        corpo_fsm.SetTrigger("movimentando");
    }
    public void RunningSlide(InputAction.CallbackContext context)
    {
        Debug.Log("Slide");
        slider = !slider;
        corpo_fsm.SetTrigger("movimentando");
    }

    public void Atirar (InputAction.CallbackContext context){
        if(atirando == false){
            atirando = true;
            corpo_fsm.SetLayerWeight(1,1);
            corpo_fsm.SetTrigger("fire"); //
            StartCoroutine(disparoProjetil());
        }
    }

    IEnumerator disparoProjetil(){
        yield return new WaitForSeconds(1f); //espera esse tempo para disparar junto com a animação
        GameObject disparo = Instantiate(Projetil, transform.GetChild(transform.childCount-1).transform.position, transform.rotation); //pega o último item na hieraquia dentro do player e as transformações dele
        disparo.GetComponent<Rigidbody>().AddForce(disparo.transform.forward * 16, ForceMode.VelocityChange);
        Destroy(disparo, 2);
    }
    public void Pular(InputAction.CallbackContext context)
    {
        bool poderPular = Physics.CheckSphere(transform.GetChild(0).transform.position, 0.5f, piso, QueryTriggerInteraction.Ignore);

        if (poderPular)
        {
            corpo_fisico.AddForce(Vector3.up * pulo, ForceMode.VelocityChange);
            corpo_fsm.SetTrigger("pular");
        }
    }
    public void Correr(InputAction.CallbackContext context)
    {
        correndo = !correndo;
        // Debug.Log("Correndo");
    }

    //Esperando o pulo acabar
    public void congelar()
    {
        Debug.Log("Congelando");
        corpo_fsm.speed = 0.2f;
    }

    public void RemoverLayerFire(){
        corpo_fsm.SetLayerWeight(1,0);
        atirando = false;
    }
}
