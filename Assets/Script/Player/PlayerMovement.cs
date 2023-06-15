using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    Animator anim;
    public Actions state;
    public DeathMenu dM;
    

    public float rotationAngle;
    public float turnSmoothTime = 0.15f;
    public float speed = 8f;
    public float jForce = 6f;

    bool attack;
    bool disableControl;
    
    float horizontal;
    float vertical;
    float turnSmoothVelocity;

    private string currentState;
    
    Vector3 velocity;
    public WinScript ws;

    private void Awake()
    {
        disableControl = false;
        attack = false;
        state = Actions.Idle;
        anim = GetComponent<Animator>();
        
    }
    private void Start()
    {
        
    }
    void Update()
    {
        DoLogic();
        //print(state);
    }
   
    void DoLogic()
    {
        if(state == Actions.Idle)
        {
            //ApplyGravity();
            Idle();
            CursorStates();
            CheckForJump();
            //print("Idling");
            CheckForAttack();
            ChangeAnim("Idle");
        }
        if(state == Actions.Move)
        {
            ApplyGravity();
            Movement();
            CheckForJump();
            CheckForIdle();
            CheckForAttack();
            CursorStates();
            ChangeAnim("Walk");
        }
        if(state == Actions.Jump)
        {
            //print("in jump state");
            
            ApplyGravity();
            Movement();
            CheckForLanding();
            CheckForAttack();
            CursorStates();
            //controller.Move(velocity * Time.deltaTime);
        }
        if(state == Actions.Climb)
        {
            Climbing();
            CursorStates();
           // print("Climbing");
        }
        if(state == Actions.Exit)
        {
            StartCoroutine(Exit());
            CursorStates(); 
        }
        if(state == Actions.Attack)
        {
            
        }

        // print("state=" + state);
        // print("grounded=" + grounded );
        //print("Velocity =  " + velocity);
    }
    void Idle()
    {
        GravReset();
        if ( (Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0) && disableControl == false)
        {
            state = Actions.Move;
        }
        if( Input.GetKeyDown("c"))
        {
            state = Actions.Climb;
        }
        
    }

    void ApplyGravity()
    {
        //velocity.y = gravity;
        velocity -= new Vector3(0, 10 * Time.deltaTime, 0);
    }
    void GravReset()
    {
        if(velocity.y <= -10)
        {
            velocity.y = -9;
        }
    }
    
    void CheckForJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = Actions.Jump;
            velocity.y = jForce;
        }
    }
    void CheckForLanding()
    {
        if (controller.isGrounded)
        {
            GravReset();
            state = Actions.Idle;
        }
    }

    
    void CheckForAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeAnim("Slap");
            print("attacked");
            
            
            if (!ws.win)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
        }
    }
    
    public void Attack()
    {
        AudioManager.instance.PlaySFX("Slap");
        attack = true;
    }
    void CheckForIdle()
    {
        if ((Input.GetAxisRaw("Horizontal") == 0) && (Input.GetAxisRaw("Vertical") == 0))
        {
            GravReset();
            state = Actions.Idle;
        }

    }
    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // stores the Horizontal and Vertical input as a direction i.e W,A,S and D = forward, back, left and right //
        //Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // normalized insures speed doesn't change if moving diagonally //
         
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
       // print("direction=" + direction);

        // If input is detected proceed //
        if (direction.magnitude >= 0.1f) // Magnitude means it's current value //
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); // adds smoothing to the angles turn

            transform.rotation = Quaternion.Euler(0f, angle, 0f); // Euler = combination of 3 quanternions - similar to Vector3 // rotates the angle //

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move((moveDir.normalized * speed * Time.deltaTime)); // using the character controller component move the object in the input's direction by speed //
        }
        GravReset();
        controller.Move(velocity * Time.deltaTime);
    }

    void Climbing()
    {
         if (state == Actions.Climb)
         {
             Vector3 climb = new Vector3(0f, 2f, 0f);
             controller.Move(climb * Time.deltaTime * speed);
         }
       // print("*** i am climbing ***");
    }
    IEnumerator Exit()
    {
        Vector3 stop = new Vector3(0, 0, 0);
        
        controller.Move(stop);
        yield return new WaitForEndOfFrame();
        controller.Move(Vector3.forward * Time.deltaTime * speed);
        yield return new WaitForSecondsRealtime(0.5f);
       // print("Getting off ladder");
        state = Actions.Idle;
    }

    void ChangeAnim(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    void CursorStates()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        print("gameobject= " + gameObject.name + " player= " + col.gameObject.tag);
        if(col.gameObject.tag=="Climb")
        {
            state = Actions.Climb;
        }
        if (col.gameObject.tag == "Exit")
        {
            state = Actions.Exit;
        }
        if (col.gameObject.tag=="Enemy" && attack)
        {
            col.gameObject.GetComponent<EnemyScript>().EnemyTakeDamage(30);
            attack = false;

            // How to check if col doesn't have script
        }
    }
    
}
