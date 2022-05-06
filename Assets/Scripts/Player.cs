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
    }


    private void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        municao = 1;
        stamina = 50;
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
    }

    private void Update()
    {
        Cursor.visible = false;

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


        //todo objeto que for ser atingido pelo raycast tem que ter algum tipo de collider
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
        if (Input.GetButtonUp("Fire2")) 
        {
            print(tempopararolamento);
            correr = false;
            if (tempopararolamento < 0.2f && stamina > 10)
            {
                dash = true;
                stamina = stamina - 10; 
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
            animador.SetTrigger("cambalhota");
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
        animador.SetBool("idle_Down", idlebai);
        animador.SetBool("idle_Left", idleesq);
        animador.SetBool("idle_Right", idledir);


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

       


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direcao = "cima";
            andarfrent = true;
            andartras = false;
            andaresquerda = false;
            andardireita = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direcao = "baixo";
            andarfrent = false;
            andartras = true;
            andaresquerda = false;
            andardireita = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direcao = "esquerda";
            andarfrent = false;
            andartras = false;
            andaresquerda = true;
            andardireita = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direcao = "direita";
            andarfrent = false;
            andartras = false;
            andaresquerda = false;
            andardireita = true;
        }

    }
}