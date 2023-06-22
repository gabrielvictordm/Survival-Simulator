using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,1,0); //ficar rodando
    }

    void OnTriggerEnter(Collider col){
        if(col.tag == "Player"){
            Destroy(gameObject); //destruir quando o player tocar nele
        }
    }
}
