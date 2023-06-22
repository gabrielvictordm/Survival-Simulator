using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaWinMAnagement : MonoBehaviour
{
    public GameObject TelaWin;
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            // SceneManager.LoadScene("Ganhou");

            TelaWin.SetActive(true);


        }
    }
}
