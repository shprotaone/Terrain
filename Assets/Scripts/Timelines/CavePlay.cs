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

    PlayableDirector playable;
    PlayerController playerController;
    DialogTrigger dialogTrigger;
    Animation doorOpen;
    Task task;

    bool paused = false;
    bool isOpened = false;
    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
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
        playerController.enabled = false;
        paused = false;
      
    }
    public void ReturnControl()
    {
        playerController.enabled = true;
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
}
