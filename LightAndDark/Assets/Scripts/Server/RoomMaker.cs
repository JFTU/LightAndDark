using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomMaker : MonoBehaviourPunCallbacks
{
    public InputField inputMake;
    public InputField inputJoin;
    public Text errorText;
    public void RoomMake()
    {
        print(inputMake.text);
        if (inputMake.text != "")
        {
            PhotonNetwork.CreateRoom(inputMake.text);
        }
        else
        {
            errorText.enabled = true;
        }
    }

    public void RoomJoin()
    {
        if (inputJoin.text != "")
        {
            PhotonNetwork.JoinRoom(inputJoin.text);
        }
        else
        {
            errorText.enabled = true;
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
