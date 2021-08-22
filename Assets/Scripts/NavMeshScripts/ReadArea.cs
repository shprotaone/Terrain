using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Считывает зону и передает боту
/// </summary>
public class ReadArea : MonoBehaviour
{
    private string zoneName;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Visitor"))
        {
            other.GetComponent<BotController>().Zone = gameObject.name;
        }
    }
}
