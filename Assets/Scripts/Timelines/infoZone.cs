using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoZone : MonoBehaviour
{
    public GameObject message;
    public bool reactivate;
    bool activated = false;

    void Start()
    {
        message.SetActive(false);
    }

    void OnTriggerStay(Collider other)
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

    void Again()
    {
        if (reactivate)
        {
            activated = false;
        }
    }
}
