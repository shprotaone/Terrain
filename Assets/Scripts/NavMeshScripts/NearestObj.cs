using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NearestObj : MonoBehaviour
{
    [TagSelector]
    public string objectTag;

    [SerializeField] private string nearest;

    private GameObject[] objects;
    private GameObject closest;

    void Start()
    {
        objects = GameObject.FindGameObjectsWithTag(objectTag);
    }

    void Update()
    {
        nearest = FindClosestObject().name;
    }

    public GameObject FindClosestObject()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject gameObject in objects)
        {
            Vector3 diff = gameObject.transform.position - position;
            float currDistance = diff.sqrMagnitude;
            if (currDistance < distance)
            {
                closest = gameObject;
                distance = currDistance;               
            }
        }
        return closest;
    }

    
}
