using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bau : MonoBehaviour
{
    public GameObject espada_escudo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(espada_escudo,transform.position,transform.rotation);
        Destroy(this.gameObject);
    }
}
