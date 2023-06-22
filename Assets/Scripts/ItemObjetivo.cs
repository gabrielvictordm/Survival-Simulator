using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjetivo : MonoBehaviour
{
    public string NomeItem;
    public GameObject AlvoCallback;
    public string Metodo;
    private GameController GameControllerObject = null;
    // Start is called before the first frame update
    void Start()
    {
        GameControllerObject = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,1,0); //ficar rodando
    }

    void OnTriggerEnter(Collider col){
        if(col.tag == "Player"){
            Destroy(gameObject); //destruir quando o player tocar nele

            if(GameControllerObject != null){
                GameControllerObject.ObjetivoCompleto(NomeItem);
            }

            if (AlvoCallback && Metodo != ""){
                AlvoCallback.SendMessage(Metodo);
            }
        }
    }
}
