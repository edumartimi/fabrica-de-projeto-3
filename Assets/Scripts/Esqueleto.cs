using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Esqueleto : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D fisica;
    private Vector2 movement;
    public SpriteRenderer imgesqueleto;
    public Animator animador;
    private bool atacar;
    private bool idle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ataque") 
        {
            Destroy(this.gameObject);
        }
        
    }

    private void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle > 90 || angle < -90)
        {
            imgesqueleto.flipX = true;
        }
        else 
        {
            imgesqueleto.flipX = false;
        }
      
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        animador.SetBool("ataque", atacar);
        animador.SetBool("idle", idle);
        if (Vector3.Distance(transform.position, player.transform.position) < 15 && Vector3.Distance(transform.position, player.transform.position) > 1.8f)
        {
            moveCharacter(movement);
            atacar = false;
            idle = false;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < 1.9f)
        {
            atacar = true;
            idle = false;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 15.5) 
        {
            idle = true;
            
        }
    }
    void moveCharacter(Vector2 direction) 
    {
        fisica.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}




   
