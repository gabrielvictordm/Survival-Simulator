using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatTransition : MonoBehaviour
{
   public Transform Player;
   public Transform lastPosition;
   public bool azuldead;
   public bool verdedead;
   public bool vermeldead;
   public bool golemDeadd;
   public GameObject inimigoVermelho;
   public GameObject inimigoVerde;
   public GameObject inimigoAzul;
   public GameObject golem;
   public GameObject tutorial;
   
   public int currentenemy;
   
   
   public static CombatTransition instance { get; private set; }

   private void Awake()
   {
     
      
      
         instance = this;
      
        
      DontDestroyOnLoad(gameObject);
   }

   private void Start()
   {
     CheckEnemies();
   }

   void CheckEnemies()
   {
      if(azuldead==true)inimigoAzul.SetActive(false);
      if (vermeldead==true)inimigoVermelho.SetActive(false);
      if(verdedead==true)inimigoVerde.SetActive(false);
      if(golemDeadd==true)golem.SetActive(false);
   }

  public void ChangeScene()
  {
     SceneManager.LoadScene("World");
     //Player.position = lastPosition.position;
     tutorial.SetActive(false);
  }
}
