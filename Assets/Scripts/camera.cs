using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;


public class camera : MonoBehaviour
{
    public Cinemachine.CinemachineTargetGroup targetbrain;
    public GameObject Oalvo;
    public bool dividido;
    public GameObject mouse_controle;
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
                Oalvo = hit.collider.gameObject;
            }
        }

        if (Input.GetButtonDown("Enable Debug Button 2")) 
        {
            RaycastHit2D hit = Physics2D.Raycast(mouse_controle.transform.position, Vector2.zero);
            if (hit.collider != null)
            {
                Oalvo = hit.collider.gameObject;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetButtonDown("Enable Debug Button 2") && !dividido)
        {
            targetbrain.AddMember(Oalvo.transform, 1f, 5f);
            dividido = true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetButtonDown("Enable Debug Button 2") && dividido)
        {
            targetbrain.RemoveMember(Oalvo.transform);
            dividido = false;
        }
    }


}
