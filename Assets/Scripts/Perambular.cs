using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]


public class Perambular : MonoBehaviour
{
    public float velocidadePerseguicao; // velocidde do "Inimigo" na área de Spot
    public float velocidadePeramular; //velocidde do "Inimigo" passeando
    float velocidadeCorrente; //velocidade do "Inimigo" atribuída
    public float intervaloMudancaDirecao; //tempo para alterar direção
    public bool perseguePlayer; //Indica se é do tipo que persegue o player ou não

    Coroutine moverCoroutine;
    Rigidbody2D rb2D; //Armazena rigidbody2D
    Animator animator; //Armazena o componente Animator
    Transform alvoTransform = null; //Armazena o componente Trannsform do alvo
    CircleCollider2D circleCollider; //Armazena o componente de SPOT

    Vector3 posicaoFinal;
    float anguloAtual = 0; //Angulo atribuído

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        velocidadeCorrente = velocidadePeramular;
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RotinaPerambular());
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnDrawGizmos()
    {
        if(circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }

    public IEnumerator RotinaPerambular()
    {
        while(true)
        {
            EscolheNovoPontoFinal();
            if (moverCoroutine != null)
            {
                StopCoroutine(moverCoroutine);
            }
            moverCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
            yield return new WaitForSeconds(intervaloMudancaDirecao);
        }
    }

    void EscolheNovoPontoFinal()
    {
        anguloAtual += Random.Range(0, 360);
        anguloAtual = Mathf.Repeat(anguloAtual, 360);
        posicaoFinal += Vector3ParaAngulo(anguloAtual);
    }
    Vector3 Vector3ParaAngulo(float anguloEntradaGraus)
    {
        float anguloEntradaRadianos = anguloEntradaGraus * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(anguloEntradaRadianos), Mathf.Sin(anguloEntradaRadianos), 0);
    }
    public IEnumerator Mover(Rigidbody2D rbParaMover, float velocidade)
    {
        float distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
        while(distanciaFaltante > float.Epsilon)
        {
            if (alvoTransform != null)
            {
                posicaoFinal = alvoTransform.position;
            }
            if (rbParaMover != null)
            {
                animator.SetBool("Caminhando", true);
                Vector3 novaPosicao = Vector3.MoveTowards(rbParaMover.position, posicaoFinal, velocidade * Time.deltaTime);
                rb2D.MovePosition(novaPosicao);
                distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Caminhando", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && perseguePlayer)
        {
            velocidadeCorrente = velocidadePerseguicao;
            alvoTransform = collision.gameObject.transform;
            if (moverCoroutine != null)
            {
                StopCoroutine(moverCoroutine);
            }
            moverCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Caminhando", false);
            velocidadeCorrente = velocidadePeramular;
            if (moverCoroutine != null) 
            {
                StopCoroutine(moverCoroutine);
            }
            alvoTransform = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(rb2D.position, posicaoFinal, Color.red);
    }
}
