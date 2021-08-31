using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnavalTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource mainSound;
    [SerializeField] private GameObject carnaval;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainSound.Play();
            carnaval.SetActive(true);
        }
    }
}
