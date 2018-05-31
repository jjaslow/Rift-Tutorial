using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
    public GameObject CollidingObject;
    public GameObject objectInHand;
    public GameObject prefabBullet;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            CollidingObject = other.gameObject;
        }

    }

    public void OnTriggerExit(Collider other)
    {
        CollidingObject = null;
    }


    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.2 && CollidingObject)
        {
            GrabObject();
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) < 0.2 && objectInHand)
        {
            ReleaseObject();
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.2 && objectInHand.gameObject.name == "Gun")
        {
            Shoot();
        }
    }

    public void GrabObject()
    {
        objectInHand = CollidingObject;
        objectInHand.transform.SetParent(this.transform);
        objectInHand.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ReleaseObject()
    {
        objectInHand.GetComponent<Rigidbody>().isKinematic = false;
        objectInHand.transform.SetParent(null);
        objectInHand = null;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(prefabBullet, objectInHand.transform.position + objectInHand.transform.forward * 0.3f, objectInHand.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(objectInHand.transform.forward * 2000);
    }
}
