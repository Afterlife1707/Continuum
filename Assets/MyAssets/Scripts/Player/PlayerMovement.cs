using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb, rbGunHolder;
    public Camera camera;
    public Animator animator;
    public SpriteRenderer crosshair;
    private SpriteRenderer playerSR;
    public AudioSource hitEffect;
    public bool isRolling = false;
    public float rollDistance=1000;
    Vector2 movement;
    Vector2 mousePos;
    public UnityEngine.UI.Text text;

    void Start()
    {
        crosshair.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //text.text = "Angular " +rb.angularVelocity.ToString() + " Velocity " + rb.velocity.ToString();
        if (GameManager.instance.isDialogueDisplayed || GameManager.instance.levelComplete || GameManager.instance.gameComplete || PauseMenu.IsPaused)
        {
            movement = Vector2.zero;
            animator.SetFloat("Movement", 0);
            crosshair.gameObject.SetActive(false);
            return;
        }

        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.instance.isDialogueDisplayed && !isRolling && movement.magnitude!=0)
        {
            Roll();
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerSR.flipX = true;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            playerSR.flipX = false;
        }
    }

    void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        crosshair.gameObject.SetActive(true);
        crosshair.transform.position = mousePos;
        animator.SetFloat("Movement", movement.sqrMagnitude);
    }

    void Roll()
    { 
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        isRolling = true;
        animator.SetBool("Roll", true);
        rb.velocity = movement.normalized * rollDistance * Time.deltaTime;
    }


    private void FixedUpdate()
    {
        if (isRolling)
            return;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rbGunHolder.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rbGunHolder.rotation = angle;
        if (angle > 0 && angle < 90 || angle>-270 && angle<-160)
            rbGunHolder.transform.localScale = new Vector3(-1, 1, 1);
        else
            rbGunHolder.transform.localScale = Vector3.one;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Objective")
        {
            //lvl progress
            GameManager.instance.StartEndDialogues();
        }
    }
}
