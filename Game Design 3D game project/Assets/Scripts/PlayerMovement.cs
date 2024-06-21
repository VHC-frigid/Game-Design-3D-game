using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed = 20f;
    public float momentumDamping = 5f;
    private CharacterController playerCC;
    public Animator CamAnim;
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 movementVector;

    private float verticalSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask fakeGround;
    [SerializeField] private float groundedAllowance;
    [SerializeField] private float jumpHeight;
    
    //[SerializeField] private float grounded

    void Start()
    {
        playerCC = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        MovePlayer();
        CamAnim.SetBool("isWalking", isWalking);

        FakeGroundCheck();
    }

    void GetInput()
    {
        
        // if we're holding down wasd, then give us -1, 0, 1
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
        }
        else
        {
            // if we're not then give us whatever inputVector was at when it was last checked and lerp it towards zero
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);

            isWalking = false;
        }

        if (IsGrounded())
        {
            verticalSpeed = 0f;
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && IsGrounded()) verticalSpeed = jumpHeight;

        movementVector = inputVector * playerSpeed;
        movementVector.y = verticalSpeed;
    }

    void MovePlayer()
    {
        playerCC.Move(movementVector * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        if(Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit hit, (1f + groundedAllowance )/ 2f, groundMask))
        {
            return true;
        }
        
        return false;
    }

    // this probably shouldn't be handled in the player script in actual production
    private void FakeGroundCheck()
    {
        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit hit, (1f + groundedAllowance) / 2f, fakeGround))
        {
            Destroy(hit.collider.gameObject);
        }
    }
}
