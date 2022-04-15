using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float valoratualstm;
    private float valortotalstm;
    public Scrollbar staminatela;
    private float porcstamina;
    public Player jogador;
    public bool controleativado;
    public GameObject movimentocontrole;
    private bool pause;
    public GameObject Menu;
    void Awake()
    {
        controleativado = false;
    }

    // Update is called once per frame
    void Update()
    {
        valoratualstm = jogador.stamina;
        valortotalstm = jogador.totalstamina;
        porcstamina = valoratualstm / valortotalstm;
        staminatela.size = porcstamina;

        if (Input.GetAxis("RightHorizontal") != 0 || Input.GetAxis("RightVertical") != 0 && !controleativado)
        {
            controleativado = true;
            movimentocontrole.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            Menu.SetActive(true);
            pause = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            Menu.SetActive(false);
            pause = false;
            Time.timeScale = 1;
        }


    }

}
