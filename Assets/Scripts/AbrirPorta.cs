using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPorta : MonoBehaviour
{
    public string NomeObjetivo;
    private GameController GameControllerObject = null;
    // Start is called before the first frame update
    void Start()
    {
        GameControllerObject = GameObject.Find("GameController").GetComponent<GameController>();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col){
        if(col.tag == "Player" && GameControllerObject != null){
            if(GameControllerObject.EntregarObjetivo(NomeObjetivo) == true){
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
