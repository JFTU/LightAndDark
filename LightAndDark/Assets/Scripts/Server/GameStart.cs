using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameStart : MonoBehaviour
{
    public bool startGame = false;

    public GameObject Sun;
    public GameObject Moon;
    public GameObject lightLit;
    public GameObject lightUnLit;
    private GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            player = PhotonNetwork.Instantiate(Sun.name, new Vector3(-17, 0, 0), Quaternion.identity); ;

            PhotonNetwork.Instantiate(lightLit.name, new Vector3(0, 0.8f, -17), Quaternion.AngleAxis(90, new Vector3(1, 0, 0)));
            PhotonNetwork.Instantiate(lightLit.name, new Vector3(12, 0.8f, 0), Quaternion.AngleAxis(90, new Vector3(1, 0, 0)));

            PhotonNetwork.Instantiate(lightUnLit.name, new Vector3(-12, 0.8f, 0), Quaternion.AngleAxis(90, new Vector3(1, 0, 0)));
            PhotonNetwork.Instantiate(lightUnLit.name, new Vector3(0, 0.8f, 17), Quaternion.AngleAxis(90, new Vector3(1, 0, 0)));
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            player = PhotonNetwork.Instantiate(Moon.name, new Vector3(17, 0, 0), Quaternion.identity);
        }
        player.transform.GetChild(1).gameObject.GetComponent<Camera>().enabled = true;
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            startGame = true;
            player.GetComponent<PlayerDeath>().enabled = true;
            player.GetComponent<Movement>().enabled = true;
        }
    }
}
