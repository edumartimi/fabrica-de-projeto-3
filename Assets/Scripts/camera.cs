using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class camera : MonoBehaviour
{
    public Cinemachine.CinemachineTargetGroup targetbrain;
    private Camera mainCamera;
    private AudioSource musica;


    private void Start()
    {
        musica = GetComponent<AudioSource>();
        mainCamera = Camera.main;
    }

    public void irparaoMundo() 
    {
        SceneManager.LoadScene(1);
    }


   


}
