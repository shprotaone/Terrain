using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    public float jumpSpeed = 5;
    public float gravity = 9.8f;
    public float distanceRay;

    private CharacterController controller;

    [SerializeField] private float currentSpeed;

    private float speed = 2.5f;    
    private float runSpeed = 6f;    
    private float turnSmoothVelocity;
    private float directionY;
    private bool jump, walk, run, standUp = false;
    
    public bool Jump { get { return jump; } }
    public bool Walk { get { return walk; } }
    public bool Run { get { return run; } }
    public bool StandUp { get { return standUp; } }
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = speed;    
    }
    private void Update()
    {
        Movement();
        JumpMeth();
        StandUpMeth();       
    }
  
    private void JumpMeth()
    {
        Vector3 jumpDirection = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space)&&controller.isGrounded)
        {
            directionY = jumpSpeed;
            jump = true;
        }
        else jump = false;

        directionY -= gravity * Time.deltaTime;
        jumpDirection.y = directionY;

        controller.Move(jumpDirection * speed * Time.deltaTime);
    }
    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
       
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
      
            if (direction.magnitude >= 0.1)
            {
                walk = true;

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir * currentSpeed * Time.deltaTime);

                if (Input.GetKey(KeyCode.LeftShift) && walk)
                {
                    currentSpeed = runSpeed;
                    run = true;                
                }
                else
                {
                    currentSpeed = speed;
                    run = false;                
                }

            } else walk = false;

    }
    private void StandUpMeth()
    {
        if (Input.GetKeyDown(KeyCode.E) && !standUp)
        {
            standUp = true;
        }
    }   
   
   
}
