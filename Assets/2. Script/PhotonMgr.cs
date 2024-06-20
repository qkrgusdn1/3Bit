using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonMgr : MonoBehaviourPunCallbacks
{
    public List<RoomInfo> roomList;

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

    private void Start()
    {
        TryToJoinServer();
    }

    public void TryToJoinServer()
    {
        if (PhotonNetwork.IsConnected)
            return;
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
        lodingPanel.SetActive(false);
        
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

    }
    public void TryToJoinRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        this.roomList = roomList;

    }
    //�����ϴ� �Լ�
    public void CreateRoom(string roomTitle, int maxPlayer)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayer;
        PhotonNetwork.CreateRoom(roomTitle, roomOptions);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("���� ����");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("���� ����");
    }
    public void JoinRoom(RoomInfo info)
    {
        Debug.Log("�� ���� �õ�");
        PhotonNetwork.JoinRoom(info.Name);
        PhotonNetwork.LoadLevel("InGame");
        Debug.Log("Room Name : " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�� ���� ����");
    }
}
