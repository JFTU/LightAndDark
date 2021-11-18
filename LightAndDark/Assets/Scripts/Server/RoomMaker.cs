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
    public void RoomMake()
    {
        PhotonNetwork.CreateRoom(inputMake.text);
    }

    public void RoomJoin()
    {
        PhotonNetwork.JoinRoom(inputJoin.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
