using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartTimeline : MonoBehaviour
{

    private PlayableDirector director;
    public GameObject message;
    // Start is called before the first frame update
    void Start()
    {
        director = GetComponentInChildren<PlayableDirector>();
        message.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            message.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
                director.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        message.SetActive(false);
    }

}
