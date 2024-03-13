using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed = 50;
    Rigidbody2D rb;
    Vector2 input;
  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
   
    }

    void FixedUpdate(){
        rb.velocity = input.normalized * moveSpeed * Time.deltaTime;
    }
}
