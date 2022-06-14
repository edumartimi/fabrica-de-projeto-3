using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nevoa : MonoBehaviour
{
    private Rigidbody2D fisica;
    public float velonuvem;
    public float tempo_excluir;
    float tempo;
    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        fisica.velocity = transform.right * velonuvem;
        tempo = tempo + Time.deltaTime;
        if (tempo > tempo_excluir) 
        {
            Destroy(this.gameObject);
        }
    }
}
