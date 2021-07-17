using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReadArea : MonoBehaviour
{
    private string zoneName;
    public string ZoneName { get; set; }

    private void Start()
    {
        zoneName = gameObject.name;
    }
    private void OnTriggerStay(Collider other)
    {
        print(zoneName);

        if (other.CompareTag("Visitor"))
        {

            if (zoneName == "BarNav")
            {
               other.GetComponent<BotController>().TakeABottle();
            }
            else if (zoneName == "DanceNav")
            {
                other.GetComponent<BotController>().DanceStarted();
            }
            else if (zoneName == "LaungeNav")
            {
                other.GetComponent<BotController>().Drinking();
            }
            else print(other.name + " In the otherZone");
        }
    }   
}
