using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class SaloonPlay : MonoBehaviour
{
    public GameObject dynCamera;
    public GameObject mainCamera;

    PlayableDirector playableDirector;
    Task task;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        task = GetComponent<Task>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switchCamera(true);
                playableDirector.Play();
                task.isActive = true;                
            }           
        }
    }

    void switchCamera(bool secondCamera)
    {        
        mainCamera.SetActive(false);
        dynCamera.SetActive(true);        
    }
}
