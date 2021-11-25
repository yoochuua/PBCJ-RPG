using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary> Classe abstrata que indica caracteristicas similares dos personagens
public abstract class Caractere : MonoBehaviour
{
    //public int PontosDano; // versão anterior dano
    
    //public int MaxPontosDano;   //versão anterior
    public float inicioPontosDano; // valor minimo inicial de saude do player 
    public float MaxPontosDano; // valor maximo permitido de saude

    public abstract void ResetCaractere(); //caractere volta

    public virtual IEnumerator FlickerCaractere()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public abstract IEnumerator DanoCaractere(int dano, float intervalo); //verifica danos do personagem

    /*
    Metodo que tira o personagem de cena caso ele morra
    */
    public virtual void KillCaractere(){
        Destroy(gameObject);
    }
}
