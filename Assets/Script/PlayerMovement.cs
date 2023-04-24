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
    float gravity = -10.8f;
    float fgravity = -5f;
    float horizontal;
    float vertical;
    float turnSmoothVelocity;
    bool grounded;

    Vector3 velocity;

    private void Start()
    {
        state = Actions.Idle;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        
        DoLogic();
    }
   
    void DoLogic()
    {
        if(state == Actions.Idle)
        {
            Idle();
            CursorStates();
            print("Idling");
            CheckForJump();
            ApplyGravity();
        }
        if(state == Actions.Move)
        {
            Movement();
            CursorStates();
           // print("moving");
            CheckForJump();
            ApplyGravity();
        }
        if(state == Actions.Jump)
        {
            Movement();
            CheckForLanding();
            CursorStates();
           // print("jumping");
            ApplyGravity();
        }
        if(state == Actions.Climb)
        {
            Climbing();
            CursorStates();
            print("Climbing");
        }
        if(state == Actions.Exit)
        {
            StartCoroutine(Exit());
            CursorStates(); 
            
        }
       // print("state=" + state);
       // print("grounded=" + grounded );
    }
    void Idle()
    {
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
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void ApplyFalling()
    {
        velocity.y += fgravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void CheckForJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) ) //&& transform.position.y >= 1.1f)
        {
            velocity.y = jForce;
            state = Actions.Jump;
            grounded = false;
        }
    }

    void CheckForLanding()
    {
        if (grounded)
        {
            state = Actions.Idle;
        }
    }

    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // stores the Horizontal and Vertical input as a direction i.e W,A,S and D = forward, back, left and right //
        /* Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // normalized insures speed doesn't change if moving diagonally //
         if(direction.magnitude != 0)
         {
             controller.Move(direction * speed * Time.deltaTime);
             print(direction);

             float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref turnSmoothVelocity, turnSmoothTime);
             if(direction.z > 0)
             {

                 rotationAngle = 0;
                 transform.rotation = Quaternion.Euler(0, rotation, 0);
             }
             if (direction.z < 0)
             {

                 print("going backwards");
                 rotationAngle = 180;
                 transform.rotation = Quaternion.Euler(0, rotation, 0);
             }
         }*/
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        print("direction=" + direction);

        // If input is detected proceed //
        if (direction.magnitude >= 0.1f) // Magnitude means it's current value //
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); // adds smoothing to the angles turn

            transform.rotation = Quaternion.Euler(0f, angle, 0f); // Euler = combination of 3 quanternions - similar to Vector3 // rotates the angle //

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime); // using the character controller component move the object in the input's direction by speed //
        }
        
        if( (Input.GetAxisRaw("Horizontal") == 0) && (Input.GetAxisRaw("Vertical") == 0))
        {
            state = Actions.Idle;

        }
    }

    void Climbing()
    {
         if (state == Actions.Climb)
         {
             
             Vector3 climb = new Vector3(0f, 2f, 0f);
             controller.Move(climb * Time.deltaTime * speed);
         }
        print("*** i am climbing ***");
    }
    IEnumerator Exit()
    {
        Vector3 stop = new Vector3(0, 0, 0);
        
        controller.Move(stop);
        yield return new WaitForEndOfFrame();
        controller.Move(Vector3.forward * Time.deltaTime * speed);
        yield return new WaitForSecondsRealtime(0.5f);
        print("Getting off ladder");
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
    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if (col.gameObject.tag == "Floor")
        {
            grounded = true;
            print("landed on " + col.gameObject.tag);
        }
    }
}
