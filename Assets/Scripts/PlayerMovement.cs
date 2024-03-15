using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed = 50;
    private Rigidbody2D rigidBody;
    private Vector2 moveInput;
    private Vector3 mousePosition;
    private Vector3 rotationVector;
    private float rotationZ;
    private Camera mainCamera;




    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    void FixedUpdate()
    {
        Move();
        RotateToMouse();
    }

    void Move()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rigidBody.velocity = moveInput.normalized * moveSpeed * Time.deltaTime;
    }

    void RotateToMouse()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        rotationVector = mousePosition - transform.position;
        rotationZ = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ - 90);
    }
}
