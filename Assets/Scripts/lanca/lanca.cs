using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanca : MonoBehaviour
{
    public GameObject lanc;
    private float tempo;


    void Update()
    {
        tempo += Time.deltaTime;
        if (tempo>=1.5f) 
        {
            Instantiate(lanc, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
