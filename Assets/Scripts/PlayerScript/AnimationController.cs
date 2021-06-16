using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;

    bool animReady;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        playerController.enabled = false;
    }

    void Update()
    {
        animReady = animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        MovementAnim();
        StandUpAnim();
        OtherAnimation();
    }

    void MovementAnim()
    {
        if (playerController.Jump)
        {            
            animator.SetTrigger("Jump");
            print("PropJump");
        }

        if (playerController.Walk)
        {
            animator.SetBool("Walking", true);
        }
        else animator.SetBool("Walking", false);

        if (playerController.Run && playerController.Walk)
        {
            animator.SetBool("Running", true);
        }
        else animator.SetBool("Running", false);
    }
    void StandUpAnim()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Standing");            
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            playerController.enabled = true;
        }
    }
    void OtherAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && animReady)
        {
            animator.SetTrigger("Mine");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && animReady)
        {
            animator.SetTrigger("PowerAttack");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && animReady)
        {
            animator.SetTrigger("ForwardAttack");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && animReady)
        {
            animator.SetTrigger("Drinking");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && animReady)
        {
            animator.SetTrigger("Death");
        }
    }
}
