using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTNChangeScene : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Level_Elementos");
    }
}