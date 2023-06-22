using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
   public GameObject TelaDialogo;
   public Sprite Avatar;
   public string[] Dialogo;
   public GameObject CallbackObject = null;
   public string Metodo;

   private Animator corpo_fsm;
   private int index = 0;

   void Start(){
    corpo_fsm = transform.GetComponent<Animator>();
   }

   void OnTriggerEnter(Collider col){
    if(col.tag == "Player"){
        index = 0;
        corpo_fsm.SetBool("falando", true);
        TelaDialogo.SetActive(true);
        MontarDialogo();
    }
   }

   void OnTriggerExit(Collider col){
    if(col.tag == "Player"){
        index = 0;
        corpo_fsm.SetBool("falando", true);
        TelaDialogo.SetActive(false);
    }
   }

   void MontarDialogo(){
        TelaDialogo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Dialogo[index];
        TelaDialogo.transform.GetChild(1).GetComponent<Image>().sprite = Avatar;

   }
}
