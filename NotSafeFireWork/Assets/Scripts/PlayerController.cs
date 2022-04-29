using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BulletPro;

public class PlayerController : MonoBehaviour
{

    //components
    [HideInInspector]
    public Rigidbody rb;
    public GameObject pivot;
    public GameObject gun;
    public bool isPlayerA;
    
    //scene elem
    //[Header("Limits Settings")]
    private Transform leftBorder;
    private Transform rightBorder;
    private Transform upperBorder;
    private Transform bottomBorder;

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

    [Header("Sprites & Anims")]
    //[SerializeField] Sprite playerASprite;
    //[SerializeField] Sprite playerBSprite;
    public GameObject p1;
    public GameObject p2;
    public SpriteRenderer sprP1;
    public SpriteRenderer sprP2;
    public Animator playerAAnimator;
    public Animator playerBAnimator;
    [HideInInspector] public Animator animator;
    public GameObject goodReanimationFeedback;
    public GameObject reanimationFeedbackA;
    public GameObject reanimationFeedbackB;

    //sons
    private SoundManager soundManager;
    /*
     *  TODO LIST :
     *  - attaque à la batte
     *  - interraction batte x firework
     *  - d'autres patternes de tir
     *  - bouton pour changer de patternes de tir
     *  - dash forward si le perso ne bouge pas
     */

    void Start()
    {
        soundManager = SoundManager.Instance;

        rb = GetComponent<Rigidbody>();

        //set default private values
        dashCooldownStart = -dashCooldown;
        shootCooldownStart = -shootCooldown;
        fireworkStackActu = fireworkMaxStack;

        LinkCorners();

        if (GameManager.Instance.playersJoinedCount == 1)
        {
            p1.SetActive(true);
            p2.SetActive(false);
            isPlayerA = true;
            animator = playerAAnimator;
            goodReanimationFeedback = reanimationFeedbackA;
        }
        else if (GameManager.Instance.playersJoinedCount >= 2)
        {
            p1.SetActive(false);
            p2.SetActive(true);
            isPlayerA = false;
            animator = playerBAnimator;
            goodReanimationFeedback = reanimationFeedbackB;
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        //dont change direction if dashing
        if (playerStateActu != (int)PlayerState.dashing)
        {

            movementInput = context.ReadValue<Vector2>();
            //flip to right
            if(movementInput.x >= 0)
            {
                sprP1.flipX = false;
                sprP2.flipX = false;
            }
            else
            {
                sprP1.flipX = true;
                sprP2.flipX = true;
            }
        }

        if (movementInput != Vector2.zero && animator != null)
        {
            animator.SetBool("isMove", true);
        }
        else if (animator != null)
        {
            animator.SetBool("isMove", false);
        }

    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && Time.time > dashCooldownStart + dashCooldown)
        {
            dashCooldownStart = Time.time;
            playerStateActu = (int)PlayerState.dashing;
            animator.SetBool("isDash", true);
            soundManager.PlaySFX("dash", soundManager.fxSource);
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
            if (context.ReadValue<Vector2>().magnitude != 0)
            {
                gunDirection = context.ReadValue<Vector2>();
            }
            else if (context.canceled)
            {
                playerStateActu = (int)PlayerState.active;
            }
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        StartCoroutine(Shooting());
        //set rot (quaternion needed)
        if (context.started && Time.time > shootCooldownStart + shootCooldown && fireworkStackActu > 0)
        {
            shootCooldownStart = Time.time;
            fireworkStackActu--;
            gun.GetComponent<BulletPro.BulletEmitter>().Play();
            soundManager.PlaySFX("shootPlayer",soundManager.fwSource);
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
            soundManager.PlaySFX("noAmmo", soundManager.fxSource);
            //Debug.Log("Shoot cooldown not finished")
        }
        else if (context.started && fireworkStackActu <= 0)
        {
            soundManager.PlaySFX("noAmmo", soundManager.fxSource);
            //Debug.Log("No more firework to shoot");
        }

    }

    void FixedUpdate()
    {
        Move();
        RotateGun();
        ReloadFireworkStack();
    }

    public void LinkCorners()
    {
        //find scene corners
        GameObject[] limits = GameObject.FindGameObjectsWithTag("Corner");
        foreach (GameObject corner in limits)
        {
            switch (corner.name)
            {
                case "Left":
                    leftBorder = corner.transform;
                    break;
                case "Right":
                    rightBorder = corner.transform;
                    break;
                case "Up":
                    upperBorder = corner.transform;
                    break;
                case "Down":
                    bottomBorder = corner.transform;
                    break;
            }
        }
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
                    animator.SetBool("isDash", false);
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
        pivot.transform.rotation = Quaternion.Euler(0,0,(180-angle)%360f);
    }

    public void ReloadFireworkStack()
    {
        if (fireworkStackActu < fireworkMaxStack && Time.time > fireworkCooldownStart + fireworkCooldown)
        {
            fireworkStackActu++;
            //Debug.Log("New firework in stack");

            if (fireworkStackActu < fireworkMaxStack)
            {
                fireworkCooldownStart = Time.time;
            }
        }
    }

    public void OnHitByBullet(Bullet bullet, Vector3 position)
    {
        float _damages = bullet.moduleParameters.GetFloat("_PowerLevel") * .5f;
        if(isPlayerA)
		{
            GameManager.Instance.pls.PlayerATakeDamage(_damages);
		}
        else
		{
            GameManager.Instance.pls.PlayerBTakeDamage(_damages);
        }
    }

    private IEnumerator Shooting()
    {
        animator.SetBool("isFire", true);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("isFire", false);
        yield return null;
    }
}
