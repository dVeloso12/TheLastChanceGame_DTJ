using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

public class CharacterControl : MonoBehaviour
{

    [SerializeField] Camera firstPersonCamera;

    [SerializeField] LayerMask GroundLayer;

    [SerializeField] float speed = 5f;
    [SerializeField][Range(0f, 1f)] float mouseSensibility = 0.5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float maxVelocity = 5f;

    PlayerInputControl playerInputControl;
    Rigidbody playerRigidBody;

    Vector3 cameraOffset;

    CheckGrounded checkGrounded;

    //bool _isGrounded;

    float rotationX = 0f, rotationY = 0f;
    float mouseX, mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        checkGrounded = GetComponentInChildren<CheckGrounded>();
    }


    private void Awake()
    {
        cameraOffset = firstPersonCamera.transform.position - transform.position;

        playerInputControl = new PlayerInputControl();

        playerRigidBody = GetComponent<Rigidbody>();

        //playerInputControl.PlayerOnFoot.Enable();
        //playerInputControl.PlayerOnFoot.Jump.performed += Jump_performed;

    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        GetMouseMovement();
    }

    private void FixedUpdate()
    {
        //Debug.Log(_isGrounded);


        //if (CheckGrounded())
        //{
        //    Move();
        //}

        //firstPersonCamera.transform.position = transform.position + cameraOffset;
        
        if (EventSystem.current.IsPointerOverGameObject()) return;

        CameraMovement();
    }

    private void Move()
    {
        Vector2 inputMovementVector = playerInputControl.PlayerOnFoot.Movement.ReadValue<Vector2>();


        Vector3 playerMovementForce = transform.forward * inputMovementVector.y * speed * Time.deltaTime + 
                                      transform.right * inputMovementVector.x * speed * Time.deltaTime; 


        playerRigidBody.AddForce(playerMovementForce, ForceMode.Acceleration);

        float velocityX, velocityZ;
        velocityX = Mathf.Clamp(playerRigidBody.velocity.x, -maxVelocity, maxVelocity);
        velocityZ = Mathf.Clamp(playerRigidBody.velocity.z, -maxVelocity, maxVelocity);

        Vector3 clampedVelocity = new Vector3(velocityX, playerRigidBody.velocity.y, velocityZ);

        //Debug.Log("UnclampedVelocity -> " + playerRigidBody.velocity);

        playerRigidBody.velocity = clampedVelocity;

        //Debug.Log("ClampedVelocity -> " + clampedVelocity);


    }
    private void Jump_performed(InputAction.CallbackContext context)
    {

        if (CheckGrounded())
        {

            Vector3 playerJumpForce = new Vector3(0f, jumpForce, 0f);

            playerRigidBody.AddForce(playerJumpForce, ForceMode.Impulse);

        }

    }

    private void GetMouseMovement()
    {
        mouseY = Input.GetAxisRaw("Mouse Y");
        mouseX = Input.GetAxisRaw("Mouse X");

        rotationY += -mouseY * mouseSensibility;
        rotationX += mouseX * mouseSensibility;

        rotationY = Mathf.Clamp(rotationY, -90f, 90f);


    }
    private void CameraMovement()
    {

        firstPersonCamera.transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);
        transform.rotation = Quaternion.Euler(0f, rotationX, 0f);

    }

    bool CheckGrounded()
    {

        return checkGrounded.IsGrounded;

    }
}
