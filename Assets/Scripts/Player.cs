using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Caractere
{
    public Inventario inventarioPrefab; //referencia ao objeto prefab criando do Inventario
    Inventario inventario;  
    public HealthBar healthBarPrefab;
    HealthBar healthBar;
    private void Start ()
     {
        inventario = Instantiate(inventarioPrefab);
        healthBar.caractere = this;
        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
    }
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
