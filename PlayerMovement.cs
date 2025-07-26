using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Object references
    private Controls controls;
    private CharacterController controller;
    // Base values
    private Vector3 velocity;
    private Vector2 move;
    private bool isGrounded;
    // Variables
    public float WalkSpeed = 10f;
    public float gravity = -9.81f;
    public float JumpHeight = 2.0f;
    // Ground calculation variables
    public Transform ground;
    public LayerMask groundMask;
    private float distanceToGround = 0.4f;


    private void Awake()
    {
        // Initialize controls
        controls = new Controls();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Runtime parent block
        Gravity();
        Movement();
        JumpCalc();
    }

    private void Gravity()
    {
        // Gravity runtime
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Movement()
    {
        // Movement handling
        move = controls.Player.Move.ReadValue<Vector2>();
        Vector3 distance = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(distance * WalkSpeed * Time.deltaTime);
    }

    private void JumpCalc()
    {
        // Jump function
        if (controls.Player.Jump.triggered)
        {
            // Use gravity formula, apply to player velocity
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }
    // Obligatory control toggle
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}