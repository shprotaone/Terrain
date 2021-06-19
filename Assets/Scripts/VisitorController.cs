using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisitorController : MonoBehaviour
{
    [SerializeField]
    NavMeshModifierVolume navMeshVolume;
    [SerializeField]
    float x, z;   
    public GameObject currentTarget;
    public BotController botController;


    void Start()
    {
        navMeshVolume = GetComponent<NavMeshModifierVolume>();       
        x = transform.localPosition.x;
        z = transform.localPosition.z;
    }

    void Update()
    {
        ChangeTarget();
    }

    void ChangeTarget()
    {
        x = Random.Range(0, navMeshVolume.size.x);
        z = Random.Range(0, navMeshVolume.size.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentTarget.transform.localPosition = new Vector3(x, 0, z);
            
            print(currentTarget.transform.localPosition);
        }
    }
}
