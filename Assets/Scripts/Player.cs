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
    }

    private void FixedUpdate() 
    {
       
        



        if (!travamouse)
        {
            alvo = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            alvo.Normalize();
        }
        else if (travamouse) 
        {
            alvo = inimigo_travado.transform.position - transform.position;
            alvo.Normalize();
        }

        diferrence = alvo;

        float rotationZ = Mathf.Atan2(diferrence.y, diferrence.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (!correr)
        {
            Vector2 andar = new Vector2(Input.GetAxis("Horizontal") * velocidade, Input.GetAxis("Vertical") * velocidade);
            fisica.velocity = andar;
        }
        if (correr)
        {
            Vector2 andar = new Vector2(Input.GetAxis("Horizontal") * velocidade*2, Input.GetAxis("Vertical") * velocidade*2);
            fisica.velocity = andar;
        }


    }

    private void Update()
    {
        if (Input.GetKey(chargeAndShootKey))
        {
            carregando += Time.deltaTime;
        }
        if (Input.GetKeyUp(chargeAndShootKey))
        {
            tirocarregado();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            correr = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            correr = false;
        }

        if (Input.GetKeyDown(KeyCode.P) && !travamouse)
        {
            travamouse = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && travamouse)
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