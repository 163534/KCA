using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Actions state;
    

    public float rotationAngle;
    public float turnSmoothTime = 0.15f;
    public float speed = 6f;

    float jForce = 6f;
    float horizontal;
    float vertical;
    float turnSmoothVelocity;
    
    Vector3 velocity;

    private void Awake()
    {
        state = Actions.Idle;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
            
        }
        if(state == Actions.Move)
        {
            
            ApplyGravity();
            Movement();
            CheckForJump();
            CheckForIdle();
            CursorStates();
            
        }
        if(state == Actions.Jump)
        {
            //print("in jump state");
            
            ApplyGravity();
            Movement();
            CheckForLanding();
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

        // print("state=" + state);
        // print("grounded=" + grounded );
        //print("Velocity =  " + velocity);
    }
    void Idle()
    {
        GravReset();
        if ( (Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0) )
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
            velocity.y = 0;
        }
    }
    
    void CheckForJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) ) //&& transform.position.y >= 1.1f)
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
            controller.Move( (moveDir.normalized * speed * Time.deltaTime) ); // using the character controller component move the object in the input's direction by speed //
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


    void CursorStates()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Climb"))
        {
            
            state = Actions.Climb;
        }
        if (col.CompareTag("Exit"))
        {
            state = Actions.Exit;
        }
    }
    
}
