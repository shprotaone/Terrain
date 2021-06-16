using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWScript : MonoBehaviour
{
    public void Increase()
    {
        GameObject obj = this.gameObject;
        obj.transform.localScale = new Vector3(2,2,2);
    }

    public void AddForce()
    {
        GameObject obj = this.gameObject;
        Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.up * 200);
    }

    public void WantDestroy()
    {
        Destroy(this.gameObject);
    }
}
