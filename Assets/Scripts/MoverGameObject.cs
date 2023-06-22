using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverGameObject : MonoBehaviour
{
    public Vector3 destino;
    public float TempoMvimento = 1;

    public void Movimentar(){
        StartCoroutine(MoverObjeto());
    }

    IEnumerator MoverObjeto(){
        Vector3 posicaoInicial = transform.position;

        for(float f = 0; f <= TempoMvimento; f += Time.deltaTime){
            transform.position = Vector3.Lerp(posicaoInicial, posicaoInicial + destino, f / TempoMvimento);
            yield return null;
        }
        transform.position = posicaoInicial + destino;
    }
}
