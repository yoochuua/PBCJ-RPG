using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")] //Cria um menu item

///<summary>  Classe que indica os tipos diferentes de item
public class Item : ScriptableObject
{
    public string NomeObjeto; 
    public Sprite sprite;
    public int quantidade; //quantidade daquele item
    public bool empilhavel; //Se esse tipo pode ser armazenado no mesmo slot

    public enum TipoItem
    {
        MOEDA,
        HEALTH
    }
    public TipoItem tipoItem;
}
