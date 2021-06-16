using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cam;
    public float turnSmoothTime = 0.1f;

   
    float speed = 3f;    
    float runSpeed = 6f;
    [SerializeField]
    float currentSpeed;
    public float jumpSpeed = 5;
    public float gravity = 9.8f;

    float turnSmoothVelocity;
    float directionY;
    bool jump, walk, run = false;
    CharacterController controller;
    Animator animator;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentSpeed = speed;
    }

    void Update()
    {
        Movement();
        Jump();
        Animation();
        Debug.Log("Walk = " + walk);
    }

    void Jump()
    {
        Vector3 jumpDirection = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            directionY = jumpSpeed;
            jump = true;
        }
        else jump = false;

        directionY -= gravity * Time.deltaTime;
        jumpDirection.y = directionY;

        controller.Move(jumpDirection * speed * Time.deltaTime);
    }

    void Movement()
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

            if (Input.GetKey(KeyCode.LeftShift))
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

    void Animation()
    {
        if (jump)
        {            
            animator.SetTrigger("Jump");
        }

        if (walk)
        {
            animator.SetBool("Walking", true);
        }
        else animator.SetBool("Walking", false);

        if (run)
        {
            animator.SetBool("Running", true);
        }
        else animator.SetBool("Running", false);
    }
}
