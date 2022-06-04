using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float valoratualstm;
    private float valortotalstm;
    public Scrollbar staminatela;
    public Scrollbar vidaTela;
    private float porcstamina;
    public Player jogador;
    public bool controleativado;
    public GameObject movimentocontrole;
    private bool pause;
    public GameObject Menu;
    private float porcvida;
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
        porcvida = jogador.vida / jogador.totalvida;
        staminatela.size = porcstamina;
        vidaTela.size = porcvida;

       

        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            Menu.SetActive(true);
            pause = true;
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            voltar_ao_jogo();
        }


    }

    public void voltar_ao_menu() 
    {
        SceneManager.LoadScene("Menu");
    }

    public void voltar_ao_jogo()
    {
        Menu.SetActive(false);
        pause = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void reiniciar() 
    {
        SceneManager.LoadScene("Mundo");
    }

}
