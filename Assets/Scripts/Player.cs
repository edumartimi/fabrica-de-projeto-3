using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    private Rigidbody2D fisica;
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
    private bool travamouse;
    private Vector3 diferrence;
    private Vector3 alvo;
    public GameObject inimigo_travado;
    private bool dash;
    public float dash_vel;
    private bool contartempo;
    private float contartempotmp;
    public float stamina;
    public float totalstamina;
    private bool cansado;
    public GameObject alvocontrole;
    public GameObject gerenciador;
    private float tempopararolamento;
    public GameObject mouse_controle;


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
        travamouse = false;
        stamina = 50;
    }

    private void FixedUpdate() 
    {
        if (!travamouse)
        {
            if (gerenciador.GetComponent<GameManager>().controleativado)
            {
                alvo = alvocontrole.transform.position - transform.position;
                Cursor.visible = false;
            }
            else
            {
                alvo = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            }
            alvo.Normalize();

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
        else if (travamouse)
        {
            alvo = inimigo_travado.transform.position - transform.position;
            alvo.Normalize();

            if (!correr)
            {
                Vector2 andar = new Vector2(Input.GetAxis("Vertical") * velocidade * 15, Input.GetAxis("Horizontal") * velocidade * -15);
                fisica.AddRelativeForce(andar);
            }
            if (correr)
            {
                Vector2 andar = new Vector2(Input.GetAxis("Vertical") * velocidade * 2 * 15, Input.GetAxis("Horizontal") * velocidade * 2 * -15);
                fisica.AddRelativeForce(andar);
            }
            if (dash)
            {
                contartempo = true;
                Vector2 andar = new Vector2(Input.GetAxis("Vertical") * velocidade * dash_vel * 10, Input.GetAxis("Horizontal") * velocidade * dash_vel * -10);
                fisica.AddRelativeForce(andar);
            }
        }

        diferrence = alvo;

        float rotationZ = Mathf.Atan2(diferrence.y, diferrence.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        


    }

    private void Update()
    {
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

        if (Input.GetKey(chargeAndShootKey))
        {
            carregando += Time.deltaTime;
        }
        if (Input.GetKeyUp(chargeAndShootKey))
        {
            tirocarregado();
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
        }
 
      
        if (contartempo)
        {
            contartempotmp = contartempotmp+Time.deltaTime;    
        }
        if (contartempotmp > 0.2) 
        {
            contartempotmp = 0;
            contartempo = false;
            dash = false;   
        }

        if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetButtonDown("Enable Debug Button 2") && !travamouse)
        {
            travamouse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetButtonDown("Enable Debug Button 2") && travamouse)
        {
            travamouse = false;
        }


        void tirocarregado()
        {
            if (carregando >= prec_carregar && municao >0)
            {
                GameObject tmpbullet = Instantiate(bullet, posicaotiro.position, Quaternion.identity);
                tmpbullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce);
                municao--;
            }
            carregando = 0;
        }
    }
}