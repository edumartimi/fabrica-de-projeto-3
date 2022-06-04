using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    private Rigidbody2D fisica;
    public Animator animador;
    public SpriteRenderer imgplayer;

    public int velocidade;
    public Transform posicaotiro;
    public GameObject bullet;
    public int bulletForce;
    public bool podeatirar;
    public float carregando = 0f;
    private float prec_carregar = 2f;
    private KeyCode chargeAndShootKey = KeyCode.Mouse0;
    private int municao;
    private bool correr;
    private Vector3 diferrence;
    public GameObject inimigo_travado;
    private bool dash;
    public float dash_vel;
    private bool contartempo;
    private float contartempotmp;
    public float stamina;
    public float totalstamina;
    private bool cansado;
    public GameObject gerenciador;
    private float tempopararolamento;
    public GameObject mouse_controle;
    public float vida;
    public float totalvida;

    private bool andarY;
    private bool andarfrent;
    private bool andarlat;
    private bool andartras;
    private bool andardireita;
    private bool andaresquerda;
    private string direcao;
    private bool idledir;
    private bool idleesq;
    private bool idlebai;

    private bool ataque;
    private bool defesa;

    public GameObject telamorte;

    public AudioSource espadada;
   

    private bool invencibilidade;
    private float tmp_invencivel;
    public float time_invencivel;
    public float time_invencivel_dash;
    private bool invencibilidade_dash;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "lanca")
        {
            municao++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "bau")
        {
            prec_carregar = 1f;
            Destroy(collision.gameObject);
        }

        /*if (collision.gameObject.tag == "inimigo" && !invencibilidade) 
        {
            vida--;
            invencibilidade = true;
        }*/

     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hit_inimigo")
        {
            vida--;
            invencibilidade = true;
        }
    }




    private void Start()
    {
        invencibilidade = false;
        totalvida = 5;
        fisica = GetComponent<Rigidbody2D>();
        municao = 1;
        stamina = 50;
        vida = totalvida;
        Cursor.visible = false;
        Time.timeScale = 1;

    }

    private void FixedUpdate() 
    {
            if (!correr)
            {
                Vector2 andar = new Vector2(Input.GetAxis("Horizontal") * velocidade, Input.GetAxis("Vertical") * velocidade);
                fisica.velocity = andar;
            }
            if (correr)
            {
                Vector2 andar = new Vector2(Input.GetAxis("Horizontal") * velocidade * 2, Input.GetAxis("Vertical") * velocidade * 2);
                fisica.velocity = andar;
            }
            if (dash)
            {
                contartempo = true;
                Vector2 andar = new Vector2(Input.GetAxis("Horizontal") * velocidade * dash_vel , Input.GetAxis("Vertical") * velocidade * dash_vel);
                fisica.velocity = andar;
            }


        if (invencibilidade)
        {
            
            gameObject.layer = 7;
            tmp_invencivel = tmp_invencivel + Time.deltaTime;
            if (tmp_invencivel > time_invencivel)
            {
                invencibilidade = false;
                tmp_invencivel = 0;
                gameObject.layer = 0;
            }

            for (float i = 0; tmp_invencivel > i; i += 0.5f)
            {
                if (imgplayer.color.a == 0.1f)
                {
                    imgplayer.color = new Color(255, 255, 255, 255);
                }
                else if (imgplayer.color.a == 255f)
                {
                    imgplayer.color = new Color(255, 255, 255, 0.1f);
                }
            }
            
        }
        else 
        {
            imgplayer.color = new Color(255, 255, 255, 255);
        }


        if (invencibilidade_dash)
        {
            print("invensivel");
            gameObject.layer = 7;
            tmp_invencivel = tmp_invencivel + Time.deltaTime;
            if (tmp_invencivel > time_invencivel_dash)
            {
                invencibilidade_dash = false;
                tmp_invencivel = 0;
                gameObject.layer = 0;
            }
        }

    }

    private void Update()
    {
        if (vida <= 0) 
        {
            telamorte.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        


       
        if (stamina < 0.5) 
        {
            cansado = true;
            correr = false;
        }
        if (!cansado)
        {
            stamina = stamina + Time.deltaTime * 8;
        }
        else if(cansado){
            stamina = stamina + Time.deltaTime * 4;
        }

        if (stamina >= totalstamina) {
            stamina = totalstamina;
            cansado = false;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ataque = true;
            espadada.Play();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ataque = false;
        }

        if (ataque)
        {
            switch (direcao)
            {
                case "cima":
                    animador.SetTrigger("ataque_up");
                    break;
                case "baixo":
                    animador.SetTrigger("ataque_down");
                    break;
                case "esquerda":
                    animador.SetTrigger("ataque_left");
                    break;
                case "direita":
                    animador.SetTrigger("ataque_right");
                    break;
            }
        }


        if (defesa)
        {
            switch (direcao)
            {
                case "cima":
                    animador.SetTrigger("def_up");
                    break;
                case "baixo":
                    animador.SetTrigger("def_down");
                    break;
                case "esquerda":
                    animador.SetTrigger("def_left");
                    break;
                case "direita":
                    animador.SetTrigger("def_right");
                    break;
            }
        }


        //todo objeto que for ser atingido pelo raycast tem que ter algum tipo de collider
        /*
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                inimigo_travado = hit.collider.gameObject;
            }
        }

        if (Input.GetButtonDown("Enable Debug Button 2"))
        {
            RaycastHit2D hit = Physics2D.Raycast(mouse_controle.transform.position, Vector2.zero);
            if (hit.collider != null)
            {
                inimigo_travado = hit.collider.gameObject;
            }
        }
        */

        if (correr) 
        {
            stamina = stamina - Time.deltaTime*16;
        }
            
        if (Input.GetKeyDown(KeyCode.LeftShift)&& stamina>3)
        {
            correr = true;
        }
        if (Input.GetButton("Fire2")) 
        {
            correr = true;
            dash = false;
            tempopararolamento += Time.deltaTime;
        }
        if (Input.GetButtonUp("Fire2") || Input.GetKeyDown(KeyCode.Space)) 
        {
            correr = false;
            if (tempopararolamento < 0.2f && stamina > 10)
            {
                dash = true;
                stamina = stamina - 10;
                invencibilidade_dash = true;
            }
            tempopararolamento = 0;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            correr = false;
        }


        if (Input.GetKeyDown(KeyCode.Space) && stamina>10)
        {
            dash = true;
            stamina = stamina - 10;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            defesa = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            defesa = false;
        }

        if (contartempo)
        {
            contartempotmp = contartempotmp + Time.deltaTime;
        }
        if (contartempotmp > 0.2)
        {
            contartempotmp = 0;
            contartempo = false;
            dash = false;
        }

        if (dash) 
        {
            switch (direcao)
            {
                case "cima":
                    animador.SetTrigger("cambalhota_cim");
                    break;
                case "baixo":
                    animador.SetTrigger("cambalhota_bai");
                    break;
                case "esquerda":
                    animador.SetTrigger("cambalhota_esq");
                    break;
                case "direita":
                    animador.SetTrigger("cambalhota_dir");
                    break;
            }
        }


        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) 
        {
            andarfrent = false;
            andartras = false;
            andardireita = false;
            andaresquerda = false;
        }

        /*
        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
               
                andartras = false;
            }
            else
            {
                andarfrent = false;
                andartras = true;
            }
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                andardireita = true;
                andaresquerda = false;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                andardireita = false;
                andaresquerda = true;
            }
        }
        */
       
        animador.SetBool("and_frent", andarfrent);
        animador.SetBool("and_tras", andartras);
        animador.SetBool("and_direita", andardireita);
        animador.SetBool("and_esquerda", andaresquerda);
        animador.SetBool("idleDown", idlebai);
        animador.SetBool("idleLeft", idleesq);
        animador.SetBool("idleRight", idledir);

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            switch (direcao)
            {
                case "cima":
                    idledir = false;
                    idleesq = false;
                    idlebai = false;
                    break;
                case "baixo":
                    idledir = false;
                    idleesq = false;
                    idlebai = true;
                    break;
                case "esquerda":
                    idledir = false;
                    idleesq = true;
                    idlebai = false;

                    break;
                case "direita":
                    idledir = true;
                    idleesq = false;
                    idlebai = false;

                    break;
            }
        }
        else 
        {
            idledir = false;
            idleesq = false;
            idlebai = false;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            direcao = "cima";
            andarfrent = true;
            andartras = false;
            andaresquerda = false;
            andardireita = false;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            direcao = "baixo";
            andarfrent = false;
            andartras = true;
            andaresquerda = false;
            andardireita = false;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            direcao = "esquerda";
            andarfrent = false;
            andartras = false;
            andaresquerda = true;
            andardireita = false;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            direcao = "direita";
            andarfrent = false;
            andartras = false;
            andaresquerda = false;
            andardireita = true;
        }

    }
}