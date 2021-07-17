using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.PostProcessing;

public class CavePlay : MonoBehaviour
{
    public GameObject player,ksardas;
    public GameObject greenCore;
    public GameObject mainCamera;
    public GameObject door;
    
    private PlayerController playerController;
    private CharacterController characterController;
    private AnimationController animationController;

    private PlayableDirector playable;
    private DialogTrigger dialogTrigger;
    private Animation doorOpen;
    private Task task;

    [SerializeField] GameObject playerTalkPosition;

    private bool paused = false;
    private bool isOpened = false;
    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        characterController = player.GetComponent<CharacterController>();
        animationController = player.GetComponent<AnimationController>();

        doorOpen = door.GetComponent<Animation>();
        playable = GetComponent<PlayableDirector>();
        dialogTrigger = GetComponentInChildren<DialogTrigger>();
        task = GetComponent<Task>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpened)
            {
                isOpened = true;
                doorOpen.Play();
            }
            
            playable.Play();

            if (paused)
            {
                Resume();
            }
        }
    }
    public void Pause()
    {
        playable.playableGraph.GetRootPlayable(0).SetSpeed(0);
        paused = true;
    }

    public void Resume()
    {
        playable.playableGraph.GetRootPlayable(0).SetSpeed(1);
        TalkPosition();
        playerController.enabled = false;
        characterController.enabled = false;
        animationController.enabled = false;
        paused = false;          
    }
    public void ReturnControl()
    {
        playerController.enabled = true;
        characterController.enabled = true;
        animationController.enabled = true;
    }

    public void CreatePortal()
    {
        greenCore.GetComponent<ParticleSystem>().Play();
    }

    public void InitDialogue()
    {
        dialogTrigger.TriggerDialogue();
        task.isActive = true;
    }

    private void TalkPosition()
    {
        player.transform.position = playerTalkPosition.transform.position;
        player.transform.LookAt(ksardas.transform.position);
    }
}
