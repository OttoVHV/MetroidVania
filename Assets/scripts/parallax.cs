using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private float Efeito;
     
    private float PosIni, tamanho;
    
    void Start()
    {
        PosIni = transform.position.x;
        tamanho = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    
    void FixedUpdate()
    {
        float dist = cam.transform.position.x * Efeito;
        transform.position = new Vector3(PosIni + dist, transform.position.y, transform.position.z);
        float movimento = cam.transform.position.x * (1 - Efeito);

        if(movimento > PosIni + tamanho)
        {
            PosIni += tamanho;
        } else if(movimento < PosIni - tamanho)
        {
            PosIni -= tamanho;
        }
    }
}
