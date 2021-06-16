using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySignal : MonoBehaviour
{
  public void WantToDestroy()
    {
        Destroy(this.gameObject);
    }
}
