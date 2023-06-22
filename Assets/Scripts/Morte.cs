using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morte : MonoBehaviour
{
    private GameController GameControllerObject = null;
    // Start is called before the first frame update
    void Start()
    {
        GameControllerObject = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider col){
        if(col.tag == "Player" && GameControllerObject != null){
            GameControllerObject.GameOver();
        }
    }
}
