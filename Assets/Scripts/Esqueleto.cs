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

    private void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
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
        if (Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            moveCharacter(movement);
        }
    }
    void moveCharacter(Vector2 direction) 
    {
        fisica.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}




   
