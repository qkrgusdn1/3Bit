using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonMgr : MonoBehaviourPunCallbacks
{
    private static PhotonMgr instance;
    public static PhotonMgr Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "kr";
    }

    public GameObject lodingPanel;

    public void TryToJoinServer()
    {
        lodingPanel.SetActive(true);
        Debug.Log("���� ���� �õ�");
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("PhotonNetwork.CloudRegion : " + PhotonNetwork.CloudRegion);
        Debug.Log("���� ���� �Ϸ�");

        if (!PhotonNetwork.InLobby)
        {
            Debug.Log("�κ�� ���� �õ�");
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("�κ� ���� �Ϸ�");
        TryToJoinRoom();
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("InGame");
        Debug.Log("Room Name : " + PhotonNetwork.CurrentRoom.Name);
    }
    public void TryToJoinRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("OnRoomListUpdate �κ� ���� �� ����Ʈ" + roomList.Count);

    }
}
