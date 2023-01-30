using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Rig"))
        {
            print("Rig Collide");
            print("other gameObject name is " + collision.collider.gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rig"))
        {
            print("Rig Trigged");
            print("other gameObeject name is " + other.gameObject.name);
        }
    }
}
