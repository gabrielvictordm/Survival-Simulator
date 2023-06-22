using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTPlayGame : MonoBehaviour
{
   public void btAction(){
    Scene atual = SceneManager.GetActiveScene();
    SceneManager.LoadScene(atual.name);
   }
}
