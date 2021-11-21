using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Caractere : MonoBehaviour
{
    //public int PontosDano; // versão anterior dano
    
    //public int MaxPontosDano;   //versão anterior
    public float inicioPontosDano; // valor minimo inicial de saude do player 
    public float MaxPontosDano; // valor maximo permitido de saude

    public abstract void ResetCaractere();
    public abstract IEnumerator DanoCaractere(int dano, float intervalo);

    public virtual void KillCaractere(){
        Destroy(gameObject);
    }
}
