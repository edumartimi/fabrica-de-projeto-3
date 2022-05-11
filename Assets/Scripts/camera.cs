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


    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void irparaoMundo() 
    {
        SceneManager.LoadScene(1);
    }


   


}
