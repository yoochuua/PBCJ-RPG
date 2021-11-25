using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary> Classe que controla como os itens se comportam no inventário e cria slots
public class Inventario : MonoBehaviour
{
    public GameObject slotPrefab; // obejto que recebe o prefab Slot
    public const int numSlots = 5; // numero fixo de slots
    Image[] itemImagens = new Image[numSlots]; // array de imagens
    Item[] items = new Item[numSlots]; // array de itens
    GameObject[] slots = new GameObject[numSlots]; //array de slots
    
    /*Start is called before the first frame update*/
    void Start()
    {
        CriaSlots();
    }

    /*Update is called once per frame*/
    void Update()
    {
        if(VerificaInvCheio()){
            SceneManager.LoadScene("SceneBonus");
        }
    }

    /*
    Método que cria slots de acordo com número fixo
    */
    public void CriaSlots()
    {
        if(slotPrefab != null)
        {
            for(int i = 0; i<numSlots; i++){
                GameObject novoSlot = Instantiate(slotPrefab);
                novoSlot.name = "ItemSlot_" + i;
                novoSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = novoSlot;
                itemImagens[i] = novoSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }

    /*
    Método que adiciona item ao inventário. 
    Empilhando itens empilhaveis caso já exista algum outro do mesmo tipo, e caso não seja, colocando em outro slot
    */
    public bool AddItem(Item itemToAdd)
    {
        for(int i=0; i<items.Length;i++)
        {
            if(items[i]!=null && items[i].tipoItem == itemToAdd.tipoItem && itemToAdd.empilhavel == true)
            {
                items[i].quantidade = items[i].quantidade + 1;
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text quantidadeTexto = slotScript.qtdTexto;
                quantidadeTexto.enabled = true;
                quantidadeTexto.text = items[i].quantidade.ToString();
                return true; 
            }
            if(items[i] == null)
            {
                items[i] = Instantiate(itemToAdd);
                items[i].quantidade = 1;
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text quantidadeTexto = slotScript.qtdTexto;
                itemImagens[i].sprite = itemToAdd.sprite;
                quantidadeTexto.text = items[i].quantidade.ToString();
                itemImagens[i].enabled = true;
                return true;
            }
        }
        return false;
    }

    public bool VerificaInvCheio(){
        int quantidadeDeItens = 0;
        for(int i=0; i<items.Length;i++){
            if(items[i] != null){
                quantidadeDeItens++;
            }
        }
        if(quantidadeDeItens == items.Length){
            return true;
        }
        else{
            return false;
        }
    }
}
