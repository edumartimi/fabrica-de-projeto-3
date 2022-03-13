using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;


public class camera : MonoBehaviour
{
    public Cinemachine.CinemachineTargetGroup targetbrain;
    public GameObject teste;
    public bool dividido;
    private Camera mainCamera;


    private void Start()
    {
        dividido = false;
        mainCamera = Camera.main;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P) && !dividido) 
        {
            targetbrain.AddMember(teste.transform, 1f, 5f);
            dividido=true;
        }
        else if(Input.GetKeyDown(KeyCode.P) && dividido)
        {
            targetbrain.RemoveMember(teste.transform);
            dividido = false;
        }
    }


}
