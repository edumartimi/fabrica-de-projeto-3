using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nevoa_generator : MonoBehaviour
{
    public GameObject nevoa1;
    public GameObject nevoa2;
    public GameObject nevoa3;
    private Rigidbody2D fisica;
    private float tempo;
    private float tmpnuvem;
    private string direcao;
    private float aleatorio;
    private float tempoaleatorio;
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        direcao = "cima";
        tempoaleatorio = 3;
    }

    // Update is called once per frame
    void Update()
    {
        tempo = tempo + Time.deltaTime;
        tmpnuvem = tmpnuvem + Time.deltaTime;
        if (direcao == "cima" && tempo < 1) 
        {
            fisica.velocity = transform.up * 300;
        }
        else if (direcao == "baixo" && tempo < 1)
        {
            fisica.velocity = transform.up * -300; 
        }

        if (tempo > 1) 
        {
            if (direcao == "cima")
            {
                direcao = "baixo";
            }
            else 
            {
                direcao = "cima";
            }
            tempo = 0;
        }


        if (tmpnuvem > 0.05f) 
        {
            
            
            aleatorio = Random.Range(0,4);
            if (aleatorio == 1) 
            {
                Instantiate(nevoa1, transform.position,transform.rotation);
            }
            if (aleatorio == 2)
            {
                Instantiate(nevoa2, transform.position, transform.rotation);
            }
            if (aleatorio == 3)
            {
                Instantiate(nevoa3, transform.position, transform.rotation);
            }
            tmpnuvem = 0;
        }
    }
}
