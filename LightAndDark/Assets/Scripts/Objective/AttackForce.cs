using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackForce : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Moon")
        {
            Hit(other.gameObject);
        }
    }

    void Hit(GameObject other)
    {
        other.GetComponent<Movement>().is_Hit = true;
        other.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
        Destroy(gameObject);
    }
}
