using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int Vida = 10;
    public int QuantObjetivos = 0;
    public TMP_Text HUD;
    public GameObject TelaWin, TelaGameOver, Dano;
    private string[] ObjetivosConquistados;
    private int ObjetivosEntregues = 0;
    private int IndexConquista = 0;


    // Start is called before the first frame update
    void Start()
    {
        ObjetivosConquistados = new string[QuantObjetivos];
        for (int i = 0; i < ObjetivosConquistados.Length; i++)
        {
            ObjetivosConquistados[i] = "--";
        }
        AtualizarHUD();
    }

    // Update is called once per frame
    public void AtualizarHUD()
    {
        string retorno = "Objetivos: ";
        for (int i = 0; i < ObjetivosConquistados.Length; i++)
        {
            retorno += ObjetivosConquistados[i] + " | ";
        }

        HUD.text = retorno+" Vida: " + Vida;
    }

    public void DescontarVida()
    {
        Dano.transform.GetComponent<Image>().enabled = true;
        StartCoroutine(desligarDano());
        Vida--;

        if (Vida < 0)
        {
            Vida = 0;
        }
        if (Vida == 0)
        {
            GameOver();
            AtualizarHUD();
        }
    }

    IEnumerator desligarDano()
    {
        yield return new WaitForSeconds(.3f);
        Dano.transform.GetComponent<Image>().enabled = false;
        AtualizarHUD();
    }

    public void GameOver()
    {
        TelaGameOver.SetActive(true);
    }

    public void Ganhou()
    {
        TelaWin.SetActive(true);
    }

    public void ObjetivoCompleto(string name)
    {
        ObjetivosConquistados[IndexConquista] = name;
        IndexConquista++;
        AtualizarHUD();
    }

    public bool EntregarObjetivo(string name)
    {
        for (int i = 0; i < ObjetivosConquistados.Length; i++)
        {

            if (ObjetivosConquistados[i] == name)
            {
                ObjetivosEntregues++;
                if (ObjetivosEntregues == QuantObjetivos)
                {
                    StartCoroutine(DispararWin());
                }
                return true;

            }
        }
        return false;
    }

    IEnumerator DispararWin()
    {
        yield return new WaitForSeconds(2);
        Ganhou();
    }

}
