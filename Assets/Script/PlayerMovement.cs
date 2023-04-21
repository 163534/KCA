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
    Vector3 velocity;
    LayerMask groundLayerMask;

    public float speed = 6f;
    float jForce = 6f;
    float gravity = -10.8f;

    float horizontal;
    float vertical;


    public Transform cam;
    [SerializeField]
    private Transform target;
    public Actions state;
    bool grounded;
    bool Jumping;
    //bool climbing;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Start()
    {
        state = Actions.Idle;
        groundLayerMask = LayerMask.GetMask("Ground");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        
        DoLogic();
    }
    private void LateUpdate()
    {
        


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
            print("moving");
            CheckForJump();
            ApplyGravity();
        }
        if(state == Actions.Jump)
        {
            Movement();
            CheckForLanding();
            CursorStates();
            print("jumping");
            ApplyGravity();
        }
        if(state == Actions.Climb)
        {
            Climbing();
            CursorStates();
            print("Climbing");
        }

       // print("state=" + state);
       // print("grounded=" + grounded );
    }
    void Idle()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 && !grounded)
        {
            state = Actions.Move;
        }
       
        
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
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
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // normalized insures speed doesn't change if moving diagonally //

       // Vector3 jump = new Vector3(horizontal,1f, vertical).normalized;

        // If input is detected proceed //
        if (direction.magnitude >= 0.1f) // Magnitude means it's current value //
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f); // Euler = combination of 3 quanternions - similar to Vector3 //

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime); // using the character controller component move the object in the input's direction by speed //
        }
        
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            state = Actions.Idle;
        }

    }
  
    void Climbing()
    {
       /* if (climbing)
        {
            print("climbing up");
            Vector3 climb = new Vector3(0f, 2f, 0f);
            controller.Move(climb * Time.deltaTime * speed);
        }*/
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
            print("climbing");
           
        }
        else
        {
            print("no longer climbing");
           
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
