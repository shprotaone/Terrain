using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoZone : MonoBehaviour
{
    public GameObject message;
    public bool reactivate;
    private bool activated = false;

    private void Start()
    {
        message.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&!activated)
        {
            message.SetActive(true);
            activated = true;
        }
        Again();

    }

    private void OnTriggerExit(Collider other)
    {
        message.SetActive(false);
    }

    private void Again()
    {
        if (reactivate)
        {
            activated = false;
        }
    }
}
