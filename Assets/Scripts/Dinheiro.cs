using UnityEngine;
using System.Collections;
public class Dinheiro : MonoBehaviour {

void Start () {
    transform.position = new Vector3 (DATA.posX, DATA.posY, DATA.posZ);
    }
void Update () {
    DATA.posX = transform.position.x;
    DATA.posY = transform.position.y;
    DATA.posZ = transform.position.z;
    if (Input.GetKeyDown ("z")) {
        PlayerPrefs.SetFloat ("POSX", DATA.posX);
        PlayerPrefs.SetFloat ("PosY", DATA.posY);
        PlayerPrefs. SetFloat ("PosZ", DATA.posZ);
    }
    if (Input.GetKeyDown ("g")){
        Application.LoadLevel("Menu");
        }
         if (Input.GetKeyDown ("q")) {
            Application.LoadLevel("World");
        }
    }
           
}
