using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTurning : MonoBehaviour
{
    public GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position*-1);
    }
}
