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

    public GameObject serverLodingPanel;
    public GameObject roomLodingPanel;
    private void Start()
    {
        TryToJoinServer();
    }

    public void TryToJoinServer()
    {
        
        Debug.Log("���� ���� �õ�");
        if (!PhotonNetwork.IsConnected)
        {
            serverLodingPanel.SetActive(true);
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("PhotonNetwork.CloudRegion : " + PhotonNetwork.CloudRegion);
        Debug.Log("���� ���� �Ϸ�");
        serverLodingPanel.SetActive(false);
        
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
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("�뿡 �����߽��ϴ�: " + PhotonNetwork.CurrentRoom.Name);
        SoundMgr.Instance.lobbyMusic.gameObject.SetActive(false);
        SoundMgr.Instance.inGameMusic.gameObject.SetActive(true);
        for (int i = 0; i < SoundMgr.Instance.attackSounds.Count; i++)
        {
            SoundMgr.Instance.attackSounds[i].gameObject.SetActive(true);
        }
        PhotonNetwork.LoadLevel("InGame");

    }
    public void TryToJoinRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("PhotonMgr OnRoomListUpdate");
        this.roomList = roomList;


    }
    //�����ϴ� �Լ�
    public void CreateRoom(string roomTitle, int maxPlayer)
    {
        roomLodingPanel.SetActive(true);
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
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�� ���� ����");
    }
}
