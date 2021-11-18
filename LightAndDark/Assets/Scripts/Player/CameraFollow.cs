using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerObject;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = playerObject.transform.rotation;
    }
}
