using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{
    public float tempoInicial = 0;
    public bool rodando = true;
    private TextMeshProUGUI texto;
    // Start is called before the first frame update
    void Start()
    {
        texto = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rodando){
            tempoInicial += Time.deltaTime;
            MostrarContador(tempoInicial);
        }
    }

    void MostrarContador(float tempoMostrar){
        tempoMostrar += 1;

        float minuto = Mathf.FloorToInt(tempoMostrar / 60);
        float segundo = Mathf.FloorToInt(tempoMostrar % 60);

        texto.text = "Caminhamento: "+string.Format("{0:00}:{1:00}", minuto, segundo);
    }
}
