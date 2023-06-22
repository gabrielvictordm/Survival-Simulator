using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if(col.tag == "Inimigo"){
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
