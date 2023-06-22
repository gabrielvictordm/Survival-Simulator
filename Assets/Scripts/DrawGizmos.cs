using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
   public bool LigarDesligar = true;
   void OnDrawGizmos(){
    if(LigarDesligar){
        foreach(Transform t in transform){
            Gizmos.color = new Color(0,1,0, .5f);
            Gizmos.DrawSphere(t.position, .5f);
        }
    }
   }
}
