using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string pathPrefab;
    public string pathSprite;

    public Vector3 pickPosition;
    public Vector3 pickRotation;

    Task currentTask;

    private void Start()
    {
        currentTask = GetComponent<Task>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            currentTask.isActive = true;            
        }
    }
}
