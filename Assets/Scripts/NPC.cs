using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform player;
    public Animator animador;
    public SpriteRenderer visao;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            animador.enabled = true;
            visao.enabled = true;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 10)
        {
            animador.enabled = false;
            visao.enabled = false;
        }

    }
}
