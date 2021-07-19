using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReadArea : MonoBehaviour
{
    private string zoneName;

    public string ZoneName
    {
        get { return zoneName; }
        set { zoneName = ZoneName; }
    }

    private void Start()
    {
        zoneName = gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Visitor"))
        {
            if(zoneName == "DanceNav")
            {
                other.GetComponent<BotController>().DanceStarted();
            }else if(zoneName == "LaungeNav")
            {
                other.GetComponent<BotController>().Drinking();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Visitor"))
        {
            if (zoneName == "BarNav")
            {
                other.GetComponent<BotController>().TakeABottle();         
            }           
        }
    }   


}
