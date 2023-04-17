using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    Vector3 velocity;

    public float speed = 6f;
    public Transform cam;
    [SerializeField]
    private Transform target;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Update()
    {
        Movement();
        Jump();
    }
    void FixedUpdate()
    {
    }
    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

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
        
    }
    void Jump()
    {
        float jForce = 6f;
        float gravity = -9.8f;

                

        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y >= 1.1f)
        {
            velocity.y = jForce;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    void LookAt()
    {
        transform.LookAt(target);
    }
}
