using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public GameObject alvo;
    public float suavizacao = 0.125f;
    private Vector3 velocidade = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, alvo.transform.rotation, suavizacao);
        
        transform.position = Vector3.SmoothDamp(transform.position, alvo.transform.position, ref velocidade, suavizacao);
    }
}
