using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]

///<summary> Classe que controla a movimentação dos inimigos
public class Perambular : MonoBehaviour
{
    public float velocidadePerseguicao; // velocidde do "Inimigo" na área de Spot
    public float velocidadePeramular; //velocidde do "Inimigo" passeando
    float velocidadeCorrente; //velocidade do "Inimigo" atribuída
    public float intervaloMudancaDirecao; //tempo para alterar direção
    public bool perseguePlayer; //Indica se é do tipo que persegue o player ou não
    float anguloAtual = 0; //Angulo atribuído
    public bool temDirecao; //Verifica se o inimigo é do tipo que tem uma direção de movimentos

    Coroutine moverCoroutine;
    Rigidbody2D rb2D; //Armazena rigidbody2D
    Animator animator; //Armazena o componente Animator
    Transform alvoTransform = null; //Armazena o componente Trannsform do alvo
    CircleCollider2D circleCollider; //Armazena o componente de SPOT
    Vector3 posicaoFinal; //Posição setado como a final do movimento



    /* Start is called before the first frame update*/
    void Start()
    {
        animator = GetComponent<Animator>();
        velocidadeCorrente = velocidadePeramular;
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RotinaPerambular());
        circleCollider = GetComponent<CircleCollider2D>();
    }

    /*Metodo que desenha um alvo em volta do inimigo, indicando a partir de onde ele começa a perseguir o player*/
    private void OnDrawGizmos()
    {
        if(circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }

    /*
    Método que define como os inimigos vão se mover caso não esteja perseguindo o player 
    */
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
    
    /*
    Método que seta uma posição final aleatória, caso o player não esteja perto
    */
    void EscolheNovoPontoFinal()
    {
        anguloAtual += Random.Range(0, 360);
        anguloAtual = Mathf.Repeat(anguloAtual, 360);
        posicaoFinal += Vector3ParaAngulo(anguloAtual);
    }

    /*
    Método que transforma o angulo passado para vetor
    utilizando as posições em x = cos(angulo) e y = sin(angulo) 
    e como não temos 3 dimenções nesse jogo, setando o z em 0
    */
    Vector3 Vector3ParaAngulo(float anguloEntradaGraus)
    {
        float anguloEntradaRadianos = anguloEntradaGraus * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(anguloEntradaRadianos), Mathf.Sin(anguloEntradaRadianos), 0);
    }

    /*
    Método que define a movimentação do inimigo
    */
    public IEnumerator Mover(Rigidbody2D rbParaMover, float velocidade)
    {
        float distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;

        //Enquanto não chegou a posição final
        while(distanciaFaltante > float.Epsilon)
        {
            //Verifica se existe algum alvo (no caso o player). Caso sim, ele muda o ponto final como sendo a possição do player
            if (alvoTransform != null)
            {
                posicaoFinal = alvoTransform.position;
            }
            if(temDirecao)
            {
                if(posicaoFinal.x >= 0)
                {
                    animator.SetBool("direita", true);
                }
                else
                {
                    animator.SetBool("direita", false);
                }
            }
            if (rbParaMover != null)
            {
                //Move o inimigo
                animator.SetBool("Caminhando", true);
                Vector3 novaPosicao = Vector3.MoveTowards(rbParaMover.position, posicaoFinal, velocidade * Time.deltaTime);
                rb2D.MovePosition(novaPosicao);
                distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Caminhando", false);
    }

    /*
    Método que movimenta transforma o player em alvo caso ele entre no raio do colider e a flag perseguePlayer esteja ativa.
    */
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

    /*
    Método que verifica se o player saiu do raio do collider.
    Caso positivo, ele para de caminhar e o alvo volta para nenhum
    */
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

    /*Update is called once per frame*/
    void Update()
    {
        Debug.DrawLine(rb2D.position, posicaoFinal, Color.red); //Desenha linhas vermelhas conforme o inimigo movimenta
    }
}
