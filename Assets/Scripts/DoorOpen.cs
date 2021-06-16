using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    bool isOpened = false;
    [SerializeField]
    Animation doorOpenAnimation;

    void Start()
    {
        doorOpenAnimation = GetComponent<Animation>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOpened)
        {           
            doorOpenAnimation.Play();
            isOpened = true;
        }
    }
}
