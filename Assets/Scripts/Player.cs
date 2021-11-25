using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>  Classe que indica 
public class Player : Caractere
{
    public Inventario inventarioPrefab; //referencia ao objeto prefab criando do Inventario
    Inventario inventario;  
    public HealthBar healthBarPrefab; //Referencia ao objeto prefab da barra de vida
    HealthBar healthBar;

    public PontosDano pontosDano; // Tem o valor da "Saúde" do objeto

    private void Start ()
     {
         //Inicia a barra de vida e o inventário
        inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        pontosDano.valor = inicioPontosDano;
    }
    
    /*
    Método que verifica se o player ainda tem vida, caso sim, retira a quantidade de danos causados.
    Caso não, ele morre.
    */
    public override IEnumerator DanoCaractere(int dano, float intervalo)
    {
        while (true){
            StartCoroutine(FlickerCaractere());
            pontosDano.valor = pontosDano.valor - dano;
            if(pontosDano.valor <= float.Epsilon){
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
    /*
    Método que reinicia o jogador
    */
    public override void ResetCaractere(){
        inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        pontosDano.valor = inicioPontosDano;
    }
    /*
    Método que caso o player morra, tira ele, inventário e a barra de vida
    */
    public override void KillCaractere()
    {
        base.KillCaractere();
        Destroy(healthBar.gameObject);
        Destroy(inventario.gameObject);
    }
    /*
    Metodo que verifica se o player colidiu com algum coletável.
    Caso sim, ele exibe na tela e coloca a lógica para cada tipo:
    Se for uma moeda: Adiciona ao inventário
    Se for um coração: Adiciona os pontos de vida
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Verifica se é um objeto coletável
        if (collision.gameObject.CompareTag("Coletavel"))
        {
            Item danoObjeto = collision.gameObject.GetComponent<Consumable>().item;
            if (danoObjeto != null)
            {
                bool DeveDesaparecer = false;
                print("Acertou: " + danoObjeto.NomeObjeto);
                switch (danoObjeto.tipoItem)
                {
                    case Item.TipoItem.MOEDA:
                    //DeveDesaparecer = true; 
                    DeveDesaparecer = inventario.AddItem(danoObjeto);
                        break;
                    case Item.TipoItem.HEALTH:
                       DeveDesaparecer =  AjusteDanoObjeto(danoObjeto.quantidade);
                        break;
                    default:
                        break;
                }
                if (DeveDesaparecer)
                {
                collision.gameObject.SetActive(false); //Desativa o objeto coletado
                }
            }
        }
    }

    public bool AjusteDanoObjeto(int quantidade)
    {
        if(pontosDano.valor < MaxPontosDano){
            pontosDano.valor = pontosDano.valor + quantidade;
            print("Ajustando PD " + quantidade + ". Novo valor =" + pontosDano.valor);
            return true;
        }
        else
        {
            return false;
        }
    }
}
