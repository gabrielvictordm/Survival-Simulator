using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedra : MonoBehaviour
{
    public float Tempo = 2;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(disparar());
        }
    }

    IEnumerator disparar()
    {
        yield return new WaitForSeconds(Tempo);

        foreach (Transform t in transform)
        {
            t.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
