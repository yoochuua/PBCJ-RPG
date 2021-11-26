using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    //volta para o menu
    public void voltarMenu (){
      SceneManager.LoadScene("Menu");
  }

    //Reinicia o jogo
  public void reiniciarJogo (){
      SceneManager.LoadScene("RPG");
  }
}
