using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecondBranch
{
    public class ReadArea : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Visitor"))
            {
                other.GetComponent<BotControllerV2>().Zone = gameObject.name;
            }
        }
    }
}

