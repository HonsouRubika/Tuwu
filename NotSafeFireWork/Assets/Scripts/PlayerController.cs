using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //components
    [HideInInspector]
    public Rigidbody rb;
    public GameObject gun;
    
    //scene elem
    [Header("Limits Settings")]
    public Transform leftBorder;
    public Transform rightBorder;
    public Transform upperBorder;
    public Transform bottomBorder;

    //movement values
    [Header("Movement Settings")]
    public float movementSpeed = 100f;
    private Vector2 movementInput;
    public Vector2 gunDirection;
    //dash
    public AnimationCurve dashCurve;
    public float dashSpeed = 100f;
    public float dashDuration = 0.6f;
    public float dashCooldown = 3f;
    private float dashCooldownStart;

    //FireWorks values
    [Header("FireWorks Settings")]
    public float shootCooldown = 0.4f;
    private float shootCooldownStart;
    public float fireworkMaxStack = 3;
    public float fireworkStackActu;
    public float fireworkCooldown = 5f;
    private float fireworkCooldownStart;

    //player state
    public enum PlayerState { active, stun, dashing}
    public int playerStateActu = (int)PlayerState.active;

    /*
     *  TODO LIST :
     *  - attaque à la batte
     *  - interraction batte x firework
     *  - d'autres patternes de tir
     *  - bouton pour changer de patternes de tir
     */

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //set default private values
        dashCooldownStart = -dashCooldown;
        shootCooldownStart = -shootCooldown;
        fireworkStackActu = fireworkMaxStack;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        //dont change direction if dashing
        if (playerStateActu != (int)PlayerState.dashing)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && Time.time > dashCooldownStart + dashCooldown)
        {
            dashCooldownStart = Time.time;
            playerStateActu = (int)PlayerState.dashing;
        }
        else if (context.started && Time.time <= dashCooldownStart + dashCooldown)
        {
            //Debug.Log("Dash cooldown not finished")
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (playerStateActu != (int)PlayerState.stun)
        {
            gunDirection = context.ReadValue<Vector2>();
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        //set rot (quaternion needed)
        if (context.started && Time.time > shootCooldownStart + shootCooldown && fireworkStackActu > 0)
        {
            shootCooldownStart = Time.time;
            fireworkStackActu--;
            gun.GetComponent<BulletPro.BulletEmitter>().Play();

            //start reloading timer
            if (fireworkCooldownStart + fireworkCooldown < Time.time) {
                fireworkCooldownStart = Time.time;
            }
            else
            {
                //already reloading
            }
        }
        else if (context.started && Time.time <= shootCooldownStart + shootCooldown)
        {
            //Debug.Log("Shoot cooldown not finished")
        }
        else if (context.started && fireworkStackActu <= 0)
        {
            //Debug.Log("No more firework to shoot");
        }

    }

    void FixedUpdate()
    {
        Move();
        RotateGun();
        ReloadFireworkStack();
    }

    public void Move()
    {
        //apply from input value
        switch (playerStateActu)
        {
            case (int)PlayerState.active:
                //velocity
                rb.velocity = new Vector3(movementInput.x * movementSpeed * Time.fixedDeltaTime, movementInput.y * movementSpeed * Time.fixedDeltaTime, rb.velocity.z);
                break;
            case (int)PlayerState.stun:
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
                break;
            case (int)PlayerState.dashing:
                if(Time.time < dashCooldownStart + dashDuration)
                {
                    rb.velocity = new Vector3(movementInput.x * (movementSpeed + dashSpeed * dashCurve.Evaluate(Time.time - dashCooldownStart)) * Time.fixedDeltaTime, movementInput.y * (movementSpeed + dashSpeed * dashCurve.Evaluate(Time.time - dashCooldownStart)) * Time.fixedDeltaTime, rb.velocity.z);
                }
                else
                {
                    //dash ended, back to normal movement
                    playerStateActu = (int)PlayerState.active;
                }
                break;
        }

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

    public void RotateGun()
    {
        //gunDirection vector2D
        //gun.transform.LookAt(gun.transform.position + (Vector3)gunDirection, Vector3.up);
        float angle = Mathf.Atan2(-gunDirection.x, -gunDirection.y) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0,0,(180-angle)%360f);
    }

    public void ReloadFireworkStack()
    {
        if (fireworkStackActu < fireworkMaxStack && Time.time > fireworkCooldownStart + fireworkCooldown)
        {
            fireworkStackActu++;
            Debug.Log("New firework in stack");

            if (fireworkStackActu < fireworkMaxStack)
            {
                fireworkCooldownStart = Time.time;
            }
        }
    }

}
