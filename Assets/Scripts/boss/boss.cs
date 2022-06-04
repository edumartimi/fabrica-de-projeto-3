using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D fisica;
    private Vector2 movement;
    public SpriteRenderer bossrenderer;
    public Animator animador;
    private bool atacar;
    private bool idle;
    private float totalvida;
    private float vida;
    private bool tanadireita;
    private bool tanaesquerda;
    public GameObject fogo;
    private bool jafez;
    private bool jafez2;
    private float tempo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ataque")
        {
            vida--;
        }

    }

    private void Start()
    {
        totalvida = 10;
        vida = totalvida;
        fisica = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (vida <= 0) 
        {
            Destroy(this.gameObject);
        }

        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle > 90 || angle < -90)
        {
            tanadireita = false;
            tanaesquerda = true;
        }
        else 
        {
            tanadireita = true;
            tanaesquerda = false;
        }

        
        if (tanadireita && !jafez) 
        {
            jafez = true;
            jafez2 = false;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        if (tanaesquerda && !jafez2) 
        {
            jafez = false;
            jafez2 = true;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        animador.SetBool("ataque", atacar);
        animador.SetBool("idle", idle);
        if (Vector3.Distance(transform.position, player.transform.position) < 50 && Vector3.Distance(transform.position, player.transform.position) > 6.9f)
        {
            moveCharacter(movement);
            atacar = false;
            fogo.SetActive(false);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < 7f)
        {
            atacar = true;
            
            tempo = tempo + Time.deltaTime;
            if (tempo >= 1)
            {
                fogo.SetActive(true);
                tempo = 0;
            }

        }
    }
    void moveCharacter(Vector2 direction)
    {
        fisica.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
