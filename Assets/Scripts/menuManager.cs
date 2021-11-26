using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuManager : MonoBehaviour
{
      //inicia o jogo
  public void iniciaJogo (){
      SceneManager.LoadScene("RPG");
  }
}
