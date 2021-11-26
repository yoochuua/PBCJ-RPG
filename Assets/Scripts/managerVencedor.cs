using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class managerVencedor : MonoBehaviour
{
    //ir aos creditos
  public void vaiCreditos (){
      SceneManager.LoadScene("creditos");
  }
}
