using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisor : MonoBehaviour
{
    public bool playertocou;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            playertocou = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playertocou = false;
        }
    }




}
