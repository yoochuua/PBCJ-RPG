using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary> Classe que controla os pontos de danos causados pelo inimigo
public class Inimigo : Caractere
{
    float pontosVida; // equivalente a saúde do inimigo
    public int forcaDano; // poder de dano
    Coroutine danoCoroutine;

    /* Start is called before the first frame update*/
    void Start()
    {
        
    }

    private void OnEnable(){
        ResetCaractere();
    }

    /*
    Método que adicina danos ao player de acordo com o que foi setado em força dano
    */
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            Player player = collision.gameObject.GetComponent<Player>();
            if(danoCoroutine == null){
                danoCoroutine = StartCoroutine(player.DanoCaractere(forcaDano, 1.0f));
            }
        }
    }

    /*
    Método que verifica se o player já não está mais em contato com o inimigo, caso não, ele para o dano
    */
    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            if(danoCoroutine != null){
                StopCoroutine(danoCoroutine);
                danoCoroutine = null;
            }
        }
    }

    /*
    Método auxiliar que verifica se o player ainda tem vida, caso sim, retira a quantidade de danos causados.
    Caso o player tenha zerado a quantidade de vidas ele morre.
    */
    public override IEnumerator DanoCaractere(int dano, float intervalo)
    {
        while(true){
            pontosVida = pontosVida - dano;
            if(pontosVida <= float.Epsilon){
                KillCaractere();
                break;
            }
            if(intervalo > float.Epsilon){
                yield return new WaitForSeconds(intervalo);
            }
            else{
                break;
            }
        }
    }

    public override void ResetCaractere(){
        pontosVida = inicioPontosDano;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
