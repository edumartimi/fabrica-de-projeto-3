using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover_controle : MonoBehaviour
{
    private Rigidbody2D fisica;
    public float velocidade;
    private Vector2 mover;
    public GameObject targ;
    public int eixoX;
    public int eixoY;
    private Vector3 posiantiga;
    private Vector3 diferencapos;
    private float tempinho;
    private Vector3 novapos;

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        eixoX = (int)Input.GetAxis("RightHorizontal");
        eixoY = (int)Input.GetAxis("RightVertical");
       

        Vector2 mover = new Vector2( eixoX, eixoY * -1);
        mover.Normalize();
        if (eixoX != 0 || eixoY != 0)
        {
            fisica.velocity = mover * velocidade*2;
        }
        else
        {
            fisica.velocity = Vector2.zero;
        }
      
    }

}
