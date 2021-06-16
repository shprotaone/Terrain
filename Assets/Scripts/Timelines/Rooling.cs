using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Rooling : MonoBehaviour
{

    public GameObject player, playerParent;
    public GameObject cart;

    PlayableDirector playable;   
    Transform playerPosition;
    Task task;

    void Start()
    {
        playerPosition = player.GetComponent<Transform>();
        playable = GetComponent<PlayableDirector>();
        task = GetComponent<Task>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playable.Play();
                task.isActive = true;
            }
        }
    }
    public void CanMove()
    {            
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
            playerPosition.SetParent(playerParent.transform);            
    }

    public void CantMove()
    {
        playerPosition = player.GetComponent<Transform>();        
        playerPosition.SetParent(cart.transform);

        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;        
    }

}
