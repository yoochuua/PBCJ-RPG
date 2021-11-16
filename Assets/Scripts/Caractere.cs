using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Caractere : MonoBehaviour
{
    //public int PontosDano; // versão anterior dano
    public PontosDano pontosDano; // nova versão
    //public int MaxPontosDano;   //versão anterior
    public float inicioPontosDano; // valor minimo inicial de saude do player 
    public float MaxPontosDano; // valor maximo permitido de saude
}
