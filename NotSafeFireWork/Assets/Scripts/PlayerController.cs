using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //components
    public Rigidbody rb;

    //movement values
    [Header("Movement Settings")]
    public float movementSpeed = 100f;
    private Vector2 movementInput;

    //scene elem
    [Header("Limits Settings")]
    public Transform leftBorder;
    public Transform rightBorder;
    public Transform upperBorder;
    public Transform bottomBorder;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }


    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        //apply from input value
        rb.velocity = new Vector3(movementInput.x * movementSpeed * Time.fixedDeltaTime, movementInput.y * movementSpeed * Time.fixedDeltaTime, rb.velocity.z);

        //check if touching borders
        //left
        if (transform.position.x <= leftBorder.position.x && rb.velocity.x < 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            transform.position = new Vector3(leftBorder.position.x, transform.position.y, transform.position.z);
        }
        //right
        else if (transform.position.x >= rightBorder.position.x && rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            transform.position = new Vector3(rightBorder.position.x, transform.position.y, transform.position.z);
        }
        //up
        if(transform.position.y >= upperBorder.position.y && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            transform.position = new Vector3(transform.position.x, upperBorder.position.y, transform.position.z);
        }
        //down
        else if (transform.position.y <= bottomBorder.position.y && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            transform.position = new Vector3(transform.position.x, bottomBorder.position.y, transform.position.z);
        }

    }
}
