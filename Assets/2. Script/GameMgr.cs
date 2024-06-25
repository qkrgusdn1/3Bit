using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameMgr : MonoBehaviourPunCallbacks
{
    private static GameMgr instance;

    public static GameMgr Instance
    {
        get 
        { 
            return instance; 
        }
    }
    void Awake()
    {
        instance = this;
    }
    public List<Player> players = new List<Player>();
    public Player player;
    public GameObject diePanel;
    public GameObject connection;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
        
    }
    public void MoveClearScenes()
    {
        photonView.RPC("RPCMoveClearScenes", RpcTarget.All);
    }

    [PunRPC]
    public void RPCMoveClearScenes()
    {
        PhotonNetwork.LoadLevel("ClearScenes");
    }
    public void AddPlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            if (player.photonView.IsMine)
            {
                this.player = player;
                break;
            }
        }
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("MultiGameMgr 현재 방의 접속한 플레이어 수 확인 {PhotonNetwork.PlayerList.Length}");
        }
    }

    public void OnClickedLobbyBtn()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }



}
