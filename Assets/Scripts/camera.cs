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
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                teste = hit.collider.gameObject;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse2) && !dividido)
        {
            targetbrain.AddMember(teste.transform, 1f, 5f);
            dividido = true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse2) && dividido)
        {
            targetbrain.RemoveMember(teste.transform);
            dividido = false;
        }
    }


}
