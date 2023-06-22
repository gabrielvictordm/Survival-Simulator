using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empurrando : MonoBehaviour
{
    private bool flag_entrar = false;

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player" && flag_entrar == false)
        {
            if (col.gameObject.GetComponent<Player>().empurrando == true && col.gameObject.GetComponent<Player>().area_empurrar == true)
            {
                flag_entrar = true;
                StartCoroutine(disparar());
            }
        }
    }

    IEnumerator disparar()
    {
        yield return new WaitForSeconds(1);
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().area_empurrar = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().area_empurrar = false;
        }
    }
}
