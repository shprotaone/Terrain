using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPointNavMesh : MonoBehaviour
{
    public float range = 10.0f;
    public GameObject target;

    private void Start()
    {
        ChangePointPos(true);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 17.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                Debug.Log("Find pos: " + result);
                return true;
            }
        
        result = Vector3.zero;
        Debug.LogError("Can't find :( ");
        return false;
    }

    public void  ChangePointPos(bool activate)
    {     
        Vector3 point;
        if (RandomPoint(transform.position, range, out point) && activate)
        {
            target.transform.position = point;
        }
    }
}

